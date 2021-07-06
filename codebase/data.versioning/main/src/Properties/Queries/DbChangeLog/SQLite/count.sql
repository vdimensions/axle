select count(1)
  from migration_changelog
 where name = @name;