UPDATE [migration_changelog]
   SET [status] = @status, 
       [execution_duration] = @executionDuration, 
       [date_performed] = @datePerformed
 WHERE [id] = @id