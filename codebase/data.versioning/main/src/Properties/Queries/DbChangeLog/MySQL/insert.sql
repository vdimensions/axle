insert into `migration_changelog` 
( 
    `name`, 
    `status`, 
    `execution_duration`, 
    `date_performed` 
)
values 
( 
    @name, 
    @status, 
    @executionDuration, 
    @datePerformed
);
select LAST_INSERT_ID();
