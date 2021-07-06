SELECT COUNT(1) 
  FROM INFORMATION_SCHEMA.TABLES 
 WHERE [TABLE_NAME] = 'migration_changelog'
   AND table_catalog = DB_NAME()