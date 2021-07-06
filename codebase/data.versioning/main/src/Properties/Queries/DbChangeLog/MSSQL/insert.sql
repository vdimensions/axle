DECLARE @resultTable TABLE ( [id] INT );
INSERT INTO [migration_changelog] 
( 
    [name], 
    [status], 
    [execution_duration], 
    [date_performed] 
)
OUTPUT INSERTED.[id] INTO @resultTable
VALUES 
( 
    @name, 
    @status, 
    @executionDuration, 
    @datePerformed 
);
SELECT [id] FROM @resultTable;