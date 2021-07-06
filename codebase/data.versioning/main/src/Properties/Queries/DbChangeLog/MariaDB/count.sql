select 
    count(id) 
from 
    `migration_changelog` 
where 
    `name` = @name;