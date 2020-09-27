select count(1)
  from sqlite_master
 where type = 'table' 
   and name = 'migration_changelog';