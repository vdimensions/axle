insert into migration_changelog_lock ( id, name )
select @id AS id, @name AS name
where not exists ( select id from migration_changelog_lock where id = @id )
limit 1;
select name from migration_changelog_lock where id = @id;