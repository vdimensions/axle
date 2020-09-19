update 
    `migration_changelog`
set 
    `status` = @status, 
    `execution_duration` = @executionDuration, 
    `date_performed` = @datePerformed
 where 
    `id` = @id; 