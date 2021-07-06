INSERT INTO `migration_changelog_lock` ( `id`, `name` )
SELECT * FROM (SELECT @id, @name) AS tmp
WHERE NOT EXISTS (SELECT `id` FROM `migration_changelog_lock` WHERE `id` = @id) 
LIMIT 1;
SELECT `name` FROM `migration_changelog_lock` WHERE `id` = @id;
