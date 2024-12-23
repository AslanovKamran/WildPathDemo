

-- ----ROLES

-- CREATE TABLE roles (
-- id UUID PRIMARY KEY,							
-- name VARCHAR(50) NOT NULL UNIQUE,
-- created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
-- modified_date TIMESTAMP NULL
-- );

-- CREATE OR REPLACE FUNCTION update_modified_date()
-- RETURNS TRIGGER AS $$
-- BEGIN
--     NEW.modified_date = CURRENT_TIMESTAMP;
--     RETURN NEW;
-- END;
-- $$ LANGUAGE plpgsql;

-- CREATE TRIGGER trigger_update_modified_date
-- BEFORE UPDATE ON roles
-- FOR EACH ROW
-- EXECUTE FUNCTION update_modified_date();

-- -- Initial ROLES 
-- INSERT INTO roles (id, name) VALUES ('550e8400-e29b-41d4-a716-446655440000', 'Admin');
-- INSERT INTO roles (id, name) VALUES ('d1917bae-49c8-4baf-95d4-ff768de12dd9', 'User');














----USERS

-- 															--USERS
-- CREATE TABLE users (
--     id UUID PRIMARY KEY,                             -- Primary key, UUID type
--     username VARCHAR(50) UNIQUE NOT NULL,            -- Unique username
--     password VARCHAR(255) NOT NULL,                  -- User password
--     email VARCHAR(255) UNIQUE NOT NULL,              -- Unique email address
--     first_name VARCHAR(50) NOT NULL,                 -- First name
--     last_name VARCHAR(50) NOT NULL,                  -- Last name
--     is_active BOOLEAN NOT NULL,                      -- Active status (Maga, consider setting TRUE as default)
--     role_id UUID REFERENCES roles(id),               -- Foreign key to roles table (No need to type `FOREIGN KEY` line)
--     phone_number VARCHAR(20) UNIQUE NULL,            -- Optional unique phone number
--     last_login_date TIMESTAMP NULL,                  -- Optional last login timestamp
--     created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Creation timestamp
--     modified_date TIMESTAMP NULL                     -- Modification timestamp
-- );


-- CREATE OR REPLACE FUNCTION update_user_modified_date()
-- RETURNS TRIGGER AS $$
-- BEGIN
--     NEW.modified_date = CURRENT_TIMESTAMP;
--     RETURN NEW;
-- END;
-- $$ LANGUAGE plpgsql;

-- CREATE TRIGGER trigger_update_user_modified_date
-- BEFORE UPDATE ON users
-- FOR EACH ROW
-- EXECUTE FUNCTION update_user_modified_date();


-- --INITIAL USERS
-- INSERT INTO users (id, username, password, email, first_name, last_name, is_active, role_id, phone_number) values ('cdb91c56-50f8-4019-9af7-be77505160ab', 'admin', '12345', 'info@wildpath.az', 'Admin', 'Admin', true, '550e8400-e29b-41d4-a716-446655440000', '872-406-1920');
-- INSERT INTO users (id, username, password, email, first_name, last_name, is_active, role_id, phone_number) values ('3b8c0b31-428f-4dff-bf0e-36a79940194c', 'hheart1', 'pQ6_LbKUts', 'hheart1@admin.ch', 'Hercule', 'Heart', true, 'd1917bae-49c8-4baf-95d4-ff768de12dd9', '542-882-7213');














-- ----DIFFICULTY LEVELS
-- CREATE TABLE difficulty_levels(
--     id UUID PRIMARY KEY,                             		-- Unique identifier for the difficulty level
--     name VARCHAR(255) NOT NULL UNIQUE,                     -- Name of the difficulty level (e.g., Easy, Medium, Hard)
--     description TEXT,                                		-- Optional description of the difficulty level
--     created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 		-- Auto-populated timestamp of creation
--     modified_date TIMESTAMP NULL                   	 	-- Nullable column for tracking updates
-- );


-- CREATE OR REPLACE FUNCTION update_difficulty_modified_date()
-- RETURNS TRIGGER AS $$
-- BEGIN
--     NEW.modified_date = CURRENT_TIMESTAMP;
--     RETURN NEW;
-- END;
-- $$ LANGUAGE plpgsql;

-- CREATE TRIGGER trigger_update_difficulty_modified_date
-- BEFORE UPDATE ON difficulty_levels
-- FOR EACH ROW
-- EXECUTE FUNCTION update_difficulty_modified_date();



-- INSERT INTO difficulty_levels (id, name, description) VALUES ('123e4567-e89b-12d3-a456-426614174000', 'Easy', 'Suitable for beginners or casual users');
-- INSERT INTO difficulty_levels (id, name, description) VALUES ('123e4567-e89b-12d3-a456-426614174001', 'Medium', 'Offers a balanced challenge');
-- INSERT INTO difficulty_levels (id, name, description) VALUES ('123e4567-e89b-12d3-a456-426614174002', 'Hard', 'Challenging even for experienced users');















----CATEGORIES


-- CREATE TABLE categories(
--     id UUID PRIMARY KEY,                             -- Unique identifier for the category
--     name VARCHAR(255) NOT NULL UNIQUE,                     -- Name of the category  (e.g., Hiking, Climbing, Skiing)
--     description TEXT,                                -- Optional description of the category
--     created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Auto-populated timestamp of creation
--     modified_date TIMESTAMP NULL                    -- Nullable column for tracking updates
-- );


-- CREATE OR REPLACE FUNCTION update_category_modified_date()
-- RETURNS TRIGGER AS $$
-- BEGIN
--     NEW.modified_date = CURRENT_TIMESTAMP;
--     RETURN NEW;
-- END;
-- $$ LANGUAGE plpgsql;

-- CREATE TRIGGER trigger_update_category_modified_date
-- BEFORE UPDATE ON categories
-- FOR EACH ROW
-- EXECUTE FUNCTION update_category_modified_date();



-- INSERT INTO categories (id, name, description) VALUES ('123e4567-e89b-12d3-a456-426614174003', 'Hiking', 'Outdoor activity involving walking in nature');
-- INSERT INTO categories (id, name, description) VALUES ('123e4567-e89b-12d3-a456-426614174004', 'Climbing', 'Activity involving scaling vertical surfaces');
-- INSERT INTO categories (id, name, description) VALUES ('123e4567-e89b-12d3-a456-426614174005', 'Skiing', 'Winter sport involving sliding on snow');




----EVENTS

-- CREATE TABLE events (
--     id UUID PRIMARY KEY,
--     name VARCHAR(255) NOT NULL,
--     description TEXT,
--     start_date TIMESTAMP NOT NULL,
--     end_date TIMESTAMP NOT NULL,
--     max_participants_count INT NOT NULL,
--     current_participants_count INT DEFAULT 0,
--     difficulty_level_id UUID REFERENCES difficulty_levels(id),
--     price DECIMAL(10, 2) NOT NULL,
--     location VARCHAR(255) NOT NULL,
--     created_by_id UUID REFERENCES users(id),
--     created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Auto-populated timestamp of creation
--     modified_date TIMESTAMP NULL                    -- Nullable column for tracking updates
-- ); 



-- CREATE OR REPLACE FUNCTION update_events_modified_date()
-- RETURNS TRIGGER AS $$
-- BEGIN
--     NEW.modified_date = CURRENT_TIMESTAMP;
--     RETURN NEW;
-- END;
-- $$ LANGUAGE plpgsql;

-- CREATE TRIGGER trigger_update_event_modified_date
-- BEFORE UPDATE ON events
-- FOR EACH ROW
-- EXECUTE FUNCTION update_events_modified_date();


-- INSERT INTO events (id, name, description, start_date, end_date, max_participants_count, current_participants_count, difficulty_level_id, price, location, created_by_id)
-- VALUES
-- ('223e4567-e89b-12d3-a456-426614174001', 'Mountain Hike', 'A scenic mountain hike suitable for beginners.', '2024-12-01 08:00:00', '2024-12-01 16:00:00', 20, 5, '123e4567-e89b-12d3-a456-426614174000', 15.00, 'Blue Ridge Mountains', '3b8c0b31-428f-4dff-bf0e-36a79940194c'),
-- ('223e4567-e89b-12d3-a456-426614174002', 'Advanced Rock Climbing', 'A challenging rock climbing event for advanced climbers.', '2024-12-10 09:00:00', '2024-12-10 17:00:00', 10, 3, '123e4567-e89b-12d3-a456-426614174002', 50.00, 'Yosemite National Park', '3b8c0b31-428f-4dff-bf0e-36a79940194c'),
-- ('223e4567-e89b-12d3-a456-426614174003', 'Family Ski Day', 'A fun skiing event for families and beginners.', '2024-12-15 09:00:00', '2024-12-15 15:00:00', 50, 20, '123e4567-e89b-12d3-a456-426614174000', 30.00, 'Aspen Snowmass', 'cdb91c56-50f8-4019-9af7-be77505160ab'),
-- ('223e4567-e89b-12d3-a456-426614174004', 'Mixed Adventure Weekend', 'A mix of hiking, climbing, and skiing for all skill levels.', '2024-12-20 08:00:00', '2024-12-22 18:00:00', 30, 12, '123e4567-e89b-12d3-a456-426614174001', 120.00, 'Banff National Park', 'cdb91c56-50f8-4019-9af7-be77505160ab');























---- JUNCTION TABLE events_categories

-- CREATE TABLE events_categories (
--     event_id UUID REFERENCES events(id) ON DELETE CASCADE,
--     category_id UUID REFERENCES categories(id),
--     PRIMARY KEY (event_id, category_id)
-- );



-- INSERT INTO events_categories (event_id, category_id)
-- VALUES
-- -- Mountain Hike
-- ('223e4567-e89b-12d3-a456-426614174001', '123e4567-e89b-12d3-a456-426614174003'), -- Hiking

-- -- Advanced Rock Climbing
-- ('223e4567-e89b-12d3-a456-426614174002', '123e4567-e89b-12d3-a456-426614174004'), -- Climbing

-- -- Family Ski Day
-- ('223e4567-e89b-12d3-a456-426614174003', '123e4567-e89b-12d3-a456-426614174005'), -- Skiing

-- -- Mixed Adventure Weekend
-- ('223e4567-e89b-12d3-a456-426614174004', '123e4567-e89b-12d3-a456-426614174003'), -- Hiking
-- ('223e4567-e89b-12d3-a456-426614174004', '123e4567-e89b-12d3-a456-426614174004'), -- Climbing
-- ('223e4567-e89b-12d3-a456-426614174004', '123e4567-e89b-12d3-a456-426614174005'); -- Skiing




----Non-clustered indexes that are used to speed up selection 
-- CREATE INDEX idx_events_categories_event ON events_categories (event_id);
-- CREATE INDEX idx_events_difficulty_level ON events (difficulty_level_id);
-- CREATE INDEX idx_events_categories_category ON events_categories (category_id);














---------------------------------------------------- Fuction and stored procedures-------------------------------

----Diffuculty levels


-----#1 Get all difficulty levels

-- CREATE OR REPLACE FUNCTION get_all_difficulty_levels()
-- RETURNS TABLE (
--     id UUID,
--     name VARCHAR,
--     description TEXT,
--     created_date TIMESTAMP,
--     modified_date TIMESTAMP
-- ) AS $$
-- BEGIN
--     RETURN QUERY SELECT * FROM difficulty_levels;
-- END;
-- $$ LANGUAGE plpgsql;








-- ----#2 Get individual difficulty level by id

-- CREATE OR REPLACE FUNCTION get_difficulty_level_by_id(p_id UUID)
-- RETURNS TABLE (
--     id UUID,
--     name VARCHAR,
--     description TEXT,
--     created_date TIMESTAMP,
--     modified_date TIMESTAMP
-- ) AS $$
-- BEGIN
--     RETURN QUERY 
--     SELECT 
--         dl.id, 
--         dl.name, 
--         dl.description, 
--         dl.created_date, 
--         dl.modified_date
--     FROM difficulty_levels dl
--     WHERE dl.id = p_id;
-- END;
-- $$ LANGUAGE plpgsql;







-- ----#3 Add a new difficulty level

-- CREATE OR REPLACE FUNCTION insert_difficulty_level(
--     p_id UUID,
--     p_name VARCHAR,
--     p_description TEXT
-- ) RETURNS VOID AS $$
-- BEGIN
--     -- Ensure the name is unique (case-insensitive)
--     IF EXISTS (
--         SELECT 1
--         FROM difficulty_levels
--         WHERE LOWER(name) = LOWER(p_name)
--     ) THEN
--         -- Raise an exception if the name already exists
--         RAISE EXCEPTION 'The name "%s" is already in use by another difficulty level.', p_name
--             USING ERRCODE = '23505'; -- Unique violation SQLSTATE
--     END IF;

--     -- Insert the new category
--     INSERT INTO difficulty_levels (id, name, description)
--     VALUES (p_id, p_name, p_description);
-- END;
-- $$ LANGUAGE plpgsql;



-- ----#4 Update difficulty level

-- CREATE
-- OR REPLACE FUNCTION UPDATE_DIFFICULTY_LEVEL (P_ID UUID, P_NAME VARCHAR, P_DESCRIPTION TEXT) RETURNS VOID AS $$
-- BEGIN
--     -- Check if the record with the given id exists
--     IF NOT EXISTS (
--         SELECT 1 
--         FROM difficulty_levels 
--         WHERE id = p_id
--     ) THEN
--         -- Raise an exception if no record is found with the provided id
--         RAISE EXCEPTION 'No difficulty level found with id "%s".', p_id
--             USING ERRCODE = 'P0001';
--     END IF;

--     -- Ensure the name is unique for a different id
--     IF EXISTS (
--         SELECT 1 
--         FROM difficulty_levels 
--         WHERE LOWER(name) = LOWER (p_name) AND id != p_id
--     ) THEN
--         -- Raise an exception if the name already exists
--         RAISE EXCEPTION 'The name "%s" is already in use by another record.', p_name
--             USING ERRCODE = '23505';
--     END IF;

--     -- Update the record (excluding modified_date since it's handled by a trigger)
--     UPDATE difficulty_levels
--     SET name = p_name,
--         description = p_description
--     WHERE id = p_id;
-- END;
-- $$ LANGUAGE PLPGSQL;













----Categories

----#1 Get all categories 

-- CREATE OR REPLACE FUNCTION get_all_categories()
-- RETURNS TABLE(
--     id UUID,
--     name VARCHAR,
--     description TEXT,
--     created_date TIMESTAMP,
--     modified_date TIMESTAMP
-- ) AS $$
-- BEGIN 
--     RETURN QUERY SELECT * FROM categories;
-- END;
-- $$ LANGUAGE plpgsql;








-- ----#2 Get individual category by Id

-- CREATE OR REPLACE FUNCTION get_category_by_id(p_id UUID)
-- RETURNS TABLE (
--     id UUID,
--     name VARCHAR,
--     description TEXT,
--     created_date TIMESTAMP,
--     modified_date TIMESTAMP
-- ) AS $$
-- BEGIN
--     RETURN QUERY 
--     SELECT 
--         ctgr.id, 
--         ctgr.name, 
--         ctgr.description, 
--         ctgr.created_date, 
--         ctgr.modified_date
--     FROM categories ctgr
--     WHERE ctgr.id = p_id;
-- END;
-- $$ LANGUAGE plpgsql;














-- ----#3 Add a new category
-- CREATE OR REPLACE FUNCTION insert_category(
--     p_id UUID,
--     p_name VARCHAR,
--     p_description TEXT
-- ) RETURNS VOID AS $$
-- BEGIN
--     -- Ensure the name is unique (case-insensitive)
--     IF EXISTS (
--         SELECT 1
--         FROM categories
--         WHERE LOWER(name) = LOWER(p_name)
--     ) THEN
--         -- Raise an exception if the name already exists
--         RAISE EXCEPTION 'The name "%s" is already in use by another category.', p_name
--             USING ERRCODE = '23505'; -- Unique violation SQLSTATE
--     END IF;

--     -- Insert the new category
--     INSERT INTO categories (id, name, description)
--     VALUES (p_id, p_name, p_description);
-- END;
-- $$ LANGUAGE plpgsql;




















-- ----#4 Update category
-- CREATE OR REPLACE FUNCTION update_category(
--     p_id UUID,
--     p_name VARCHAR,
--     p_description TEXT
-- ) RETURNS VOID AS $$
-- BEGIN
--     -- Check if the category with the given id exists
--     IF NOT EXISTS (
--         SELECT 1 
--         FROM categories 
--         WHERE id = p_id
--     ) THEN
--         -- Raise an exception if no record is found with the provided id
--         RAISE EXCEPTION 'No category found with id "%s".', p_id
--             USING ERRCODE = 'P0001'; -- Custom SQLSTATE for user-defined exceptions
--     END IF;

--     -- Ensure the name is unique (case-insensitive) for a different id
--     IF EXISTS (
--         SELECT 1 
--         FROM categories 
--         WHERE LOWER(name) = LOWER(p_name) AND id != p_id
--     ) THEN
--         -- Raise an exception if the name already exists
--         RAISE EXCEPTION 'The name "%s" is already in use by another category.', p_name
--             USING ERRCODE = '23505'; -- Unique violation SQLSTATE
--     END IF;

--     -- Update the category
--     UPDATE categories
--     SET name = p_name,
--         description = p_description
--     WHERE id = p_id;
-- END;
-- $$ LANGUAGE plpgsql;


























---- Events

-- ----#1 Get all events

-- CREATE OR REPLACE FUNCTION get_all_events()
-- RETURNS TABLE(
--     id UUID,
--     name VARCHAR,
--     description TEXT,
--     start_date TIMESTAMP,
--     end_date TIMESTAMP,
--     max_participants_count INT,
--     current_participants_count INT,
--     difficulty_level_id UUID,
--     price DECIMAL,
--     location VARCHAR,
--     created_by_id UUID,
--     created_date TIMESTAMP,
--     modified_date TIMESTAMP,
--     dl_id UUID,
--     dl_name VARCHAR,
--     dl_description TEXT,
--     dl_created_date TIMESTAMP,
--     dl_modified_date TIMESTAMP,
--     categories JSON
-- ) AS $$
-- BEGIN
--     RETURN QUERY 
--     SELECT 
--         e.id, 
--         e.name, 
--         e.description, 
--         e.start_date, 
--         e.end_date, 
--         e.max_participants_count, 
--         e.current_participants_count, 
--         e.difficulty_level_id,
--         e.price, 
--         e.location, 
--         e.created_by_id, 
--         e.created_date, 
--         e.modified_date,
--         dl.id AS dl_id, 
--         dl.name AS dl_name, 
--         dl.description AS dl_description, 
--         dl.created_date AS dl_created_date, 
--         dl.modified_date AS dl_modified_date,
--         (
--             SELECT json_agg(json_build_object('id', c.id, 'name', c.name, 'description', c.description, 'createdDate', c.created_date, 'modifiedDate', c.modified_date))
--             FROM events_categories ec
--             JOIN categories c ON ec.category_id = c.id
--             WHERE ec.event_id = e.id
--         ) AS categories
--     FROM events e
--     LEFT JOIN difficulty_levels dl ON e.difficulty_level_id = dl.id;
-- END;
-- $$ LANGUAGE plpgsql;




-- ---- #2 Get individual event by Id

-- CREATE OR REPLACE FUNCTION get_event_by_id(p_event_id UUID)
-- RETURNS TABLE(
--     id UUID,
--     name VARCHAR,
--     description TEXT,
--     start_date TIMESTAMP,
--     end_date TIMESTAMP,
--     max_participants_count INT,
--     current_participants_count INT,
--     difficulty_level_id UUID,
--     price DECIMAL,
--     location VARCHAR,
--     created_by_id UUID,
--     created_date TIMESTAMP,
--     modified_date TIMESTAMP,
--     dl_id UUID,
--     dl_name VARCHAR,
--     dl_description TEXT,
--     dl_created_date TIMESTAMP,
--     dl_modified_date TIMESTAMP,
--     categories JSON
-- ) AS $$
-- BEGIN
--     RETURN QUERY 
--     SELECT 
--         e.id, 
--         e.name, 
--         e.description, 
--         e.start_date, 
--         e.end_date, 
--         e.max_participants_count, 
--         e.current_participants_count, 
--         e.difficulty_level_id,
--         e.price, 
--         e.location, 
--         e.created_by_id, 
--         e.created_date, 
--         e.modified_date,
--         dl.id AS dl_id, 
--         dl.name AS dl_name, 
--         dl.description AS dl_description, 
--         dl.created_date AS dl_created_date, 
--         dl.modified_date AS dl_modified_date,
--         (
--             SELECT json_agg(json_build_object('id', c.id, 'name', c.name, 'description', c.description, 'createdDate', c.created_date, 'modifiedDate', c.modified_date))
--             FROM events_categories ec
--             JOIN categories c ON ec.category_id = c.id
--             WHERE ec.event_id = e.id
--         ) AS categories
--     FROM events e
--     LEFT JOIN difficulty_levels dl ON e.difficulty_level_id = dl.id
--     WHERE e.id = p_event_id;
-- END;
-- $$ LANGUAGE plpgsql;







-- -- #3 Add Event procedure
-- CREATE OR REPLACE FUNCTION add_event(
--     p_event_id UUID, 
--     p_name TEXT,
--     p_description TEXT,
--     p_starts_at TIMESTAMP, -- Change here
--     p_ends_at TIMESTAMP,   -- Change here
--     p_max_participants_count INT,
--     p_current_participants_count INT,
--     p_difficulty_id UUID,
--     p_price NUMERIC(10, 2),
--     p_location TEXT,
--     p_created_by UUID,
--     p_category_ids TEXT
-- ) RETURNS UUID AS $$
-- DECLARE
--     category_id UUID;
-- BEGIN
--     -- Insert the new event
--     INSERT INTO Events (
--         Id, Name, Description, Start_Date, End_Date, Max_Participants_Count, 
--         Current_Participants_Count, Difficulty_Level_Id, Price, Location, Created_By_Id, Created_Date
--     )
--     VALUES (
--         p_event_id, p_name, p_description, p_starts_at, p_ends_at, 
--         p_max_participants_count, p_current_participants_count, p_difficulty_id, 
--         p_price, p_location, p_created_by, CURRENT_TIMESTAMP
--     );

--     -- Insert category associations
--     FOR category_id IN
--         SELECT UNNEST(string_to_array(p_category_ids, ','))::UUID
--     LOOP
--         INSERT INTO Events_Categories (Event_Id, Category_Id)
--         VALUES (p_event_id, category_id);
--     END LOOP;

--     -- Return the ID of the created event
--     RETURN p_event_id;
-- END;
-- $$ LANGUAGE plpgsql;



















-- --#4 Update an event (created by is not being updated)
-- CREATE OR REPLACE FUNCTION update_event(
--     p_event_id UUID, 
--     p_name TEXT,
--     p_description TEXT,
--     p_starts_at TIMESTAMP,
--     p_ends_at TIMESTAMP,
--     p_max_participants_count INT,
--     p_current_participants_count INT,
--     p_difficulty_id UUID,
--     p_price NUMERIC(10, 2),
--     p_location TEXT,
--     p_category_ids TEXT -- Comma-separated category IDs
-- ) RETURNS UUID AS $$
-- DECLARE
--     category_id UUID;
-- BEGIN
--     -- Check if the difficulty level exists
--     IF NOT EXISTS (SELECT 1 FROM Difficulty_Levels WHERE id = p_difficulty_id) THEN
--         RAISE EXCEPTION 'The selected difficulty level does not exist. Please choose a valid difficulty level.';
--     END IF;

--     -- Update the event
--     UPDATE Events
--     SET 
--         name = p_name,
--         description = p_description,
--         start_date = p_starts_at,
--         end_date = p_ends_at,
--         max_participants_count = p_max_participants_count,
--         current_participants_count = p_current_participants_count,
--         difficulty_level_id = p_difficulty_id,
--         price = p_price,
--         location = p_location,
--         modified_date = CURRENT_TIMESTAMP
--     WHERE id = p_event_id;

--     -- Check if the event exists
--     IF NOT FOUND THEN
--         RAISE EXCEPTION 'Event with the given ID does not exist. Please provide a valid EventId.';
--     END IF;

--     -- Delete all existing categories for this event
--     DELETE FROM Events_Categories WHERE event_id = p_event_id;

--     -- Insert new categories for the event
--     FOR category_id IN
--         SELECT DISTINCT UNNEST(string_to_array(p_category_ids, ','))::UUID
--     LOOP
--         -- Check if the category exists
--         IF NOT EXISTS (SELECT 1 FROM Categories WHERE id = category_id) THEN
--             RAISE EXCEPTION 'One or more selected categories do not exist. Please choose valid categories.';
--         END IF;

--         -- Insert the new category for the event
--         INSERT INTO Events_Categories (event_id, category_id) 
--         VALUES (p_event_id, category_id)
--         ON CONFLICT DO NOTHING; -- Prevent duplicates
--     END LOOP;

--     -- Return the updated event ID
--     RETURN p_event_id;

-- EXCEPTION
--     WHEN OTHERS THEN
--         RAISE EXCEPTION 'An error occurred during the update: %', SQLERRM;
-- END;
-- $$ LANGUAGE plpgsql;



-- --#5 Delete By Id

CREATE OR REPLACE FUNCTION delete_event(
    p_event_id UUID
) RETURNS VOID AS $$
BEGIN
    -- Delete the event
    DELETE FROM Events WHERE id = p_event_id;

    -- Check if the event was deleted
    IF NOT FOUND THEN
        RAISE EXCEPTION 'Event with the given ID does not exist.';
    END IF;
END;
$$ LANGUAGE plpgsql;
























