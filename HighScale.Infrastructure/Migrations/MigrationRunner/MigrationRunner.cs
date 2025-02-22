using Cassandra;
using Microsoft.Extensions.DependencyInjection;

namespace HighScale.Infrastructure.Migrations.MigrationRunner;

public abstract class MigrationRunner
{
    private const string MigrationScriptsPath = "../HighScale.Infrastructure/Migrations/Scripts";
    private const string CassandraScriptExtension = ".cql";
    private static readonly char[] Separator = ['\\', '/'];

    public static void RunMigrations(IServiceCollection services)
    {
        try
        {
            using var serviceProvider = services.BuildServiceProvider();
            var session = serviceProvider.GetService<ISession>()
                ?? throw new InvalidOperationException("Session for ScyllaDb is not initialize properly");

            session.CreateKeyspaceIfNotExists("high_scale", new Dictionary<string, string>()
            {
                { "class", "SimpleStrategy" },
                { "replication_factor", "2" },
            });

            session.Execute(new SimpleStatement(
                "CREATE TABLE IF NOT EXISTS high_scale.migrations (migration_id text PRIMARY KEY, applied_at timestamp)"
                ));

            //Get the current migrations
            var migrations = session.Execute(new SimpleStatement(
                "SELECT migration_id from high_scale.migrations"
            ));
            var appliedMigrations = migrations.Select(x => x.GetValue<string>("migration_id")).ToHashSet();

            //Apply the new migrations
            var migrationScripts = Directory.EnumerateFiles(MigrationScriptsPath, $"*{CassandraScriptExtension}")
                .Select(filepath => new { fileName = filepath.Split(Separator).Last(), filePath = filepath });

            foreach (var script in migrationScripts)
            {
                var scriptWithoutExtension = script.fileName.Replace(CassandraScriptExtension, "");

                if (appliedMigrations.Contains(scriptWithoutExtension))
                    continue;

                var cqlScripts = File.ReadAllText(script.filePath)
                    .Split(';')
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();

                foreach (var cql in cqlScripts)
                {
                    session.Execute(new SimpleStatement(cql));
                }
    
                session.Execute(new SimpleStatement(
                    "INSERT INTO high_scale.migrations (migration_id, applied_at) VALUES (?, ?)",
                    scriptWithoutExtension, DateTime.UtcNow
                ));
            }
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("Could not run migrations", ex);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could not run migrations", ex);
        }
    }
}