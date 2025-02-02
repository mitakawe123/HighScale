CREATE TABLE IF NOT EXISTS high_scale.orders(
    id bigint,
    user_id bigint,
    product_ids list<bigint>,
    quantity int,
    total_price decimal,
    order_date timestamp,
    PRIMARY KEY ( (user_id), order_date ))
    WITH CLUSTERING ORDER BY (order_date desc);

CREATE TABLE IF NOT EXISTS high_scale.products(
    id bigint,
    name text,
    price decimal,
    stock int,
    category text,
    created_at timestamp,
    PRIMARY KEY ( (category, price), created_at ))
    WITH CLUSTERING ORDER BY (created_at desc);

CREATE TABLE high_scale.logs (
    id bigint,
    level TEXT,
    message TEXT,
    created_at TIMESTAMP,
    PRIMARY KEY ( level, created_at )) 
    WITH CLUSTERING ORDER BY (created_at desc);

