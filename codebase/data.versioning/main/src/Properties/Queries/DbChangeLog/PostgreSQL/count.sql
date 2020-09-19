SELECT 
    COUNT(*)::INT 
FROM 
    migration_changelog
WHERE 
    name = @name