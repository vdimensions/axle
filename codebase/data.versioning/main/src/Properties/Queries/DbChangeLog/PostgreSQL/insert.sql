INSERT INTO migration_changelog 
(
    name, 
    status, 
    execution_duration, 
    date_performed 
)
VALUES 
(
    @name, 
    @status, 
    @executionDuration, 
    @datePerformed 
)
RETURNING id;
