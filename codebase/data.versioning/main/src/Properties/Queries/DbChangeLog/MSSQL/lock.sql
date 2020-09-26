INSERT INTO [migration_changelog_lock] ( [id], [name] )
SELECT @id, @name
WHERE NOT EXISTS ( SELECT [id] FROM [migration_changelog_lock] WHERE [id] = @id );
SELECT [name] FROM [migration_changelog_lock] WHERE [id] = @id;