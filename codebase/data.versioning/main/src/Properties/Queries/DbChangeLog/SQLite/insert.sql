insert into migration_changelog 
(
    name, 
    status, 
    execution_duration, 
    date_performed 
)
values 
(
    @name, 
    @status, 
    @executionDuration, 
    @datePerformed 
);
select last_insert_rowid();
