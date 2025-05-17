ALTER TABLE teacherUsers
ADD COLUMN class_code VARCHAR(255);

ALTER TABLE teacherUsers
ADD CONSTRAINT unique_class_code UNIQUE (class_code);

ALTER TABLE studentUsers
ADD UNIQUE (class_code);

ALTER TABLE studentUsers
RENAME COLUMN createdClass TO joined_class;
