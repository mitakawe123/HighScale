CREATE TABLE IF NOT EXISTS high_scale.users(
  id bigint PRIMARY KEY,
  name text,
  age smallint,
  address text,
  created_at timestamp,
  updated_at timestamp  
);