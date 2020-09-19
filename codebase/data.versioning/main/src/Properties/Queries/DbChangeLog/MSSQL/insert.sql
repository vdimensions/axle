DECLARE @resultTable TABLE ( [ID] INT );
INSERT INTO [migration_changelog] 
( 
    [name], 
    [status], 
    [execution_duration], 
    [date_performed] 
)
OUTPUT INSERTED.[ID] INTO @resultTable
VALUES 
( 
    @name, 
    @status, 
    @executionDuration, 
    @datePerformed 
);
SELECT [ID] FROM @resultTable;