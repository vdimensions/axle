CREATE TABLE IF NOT EXISTS `migration_changelog` (
    `id`                  INT             NOT NULL        AUTO_INCREMENT,
    `name`                VARCHAR(255)    NOT NULL,
    `status`              SMALLINT        NOT NULL,
    `execution_duration`  BIGINT          NOT NULL,
    `date_performed`      DATETIME        NOT NULL,
    PRIMARY KEY ( `id` )
);

CREATE UNIQUE INDEX IF NOT EXISTS `migration_changelog_ux01` ON `migration_changelog` ( `name` ASC );
CREATE        INDEX IF NOT EXISTS `migration_changelog_ix01` ON `migration_changelog` ( `date_performed` ASC );
CREATE        INDEX IF NOT EXISTS `migration_changelog_ix02` ON `migration_changelog` ( `execution_duration` );


SET @sql := 
    CASE 
    WHEN NOT EXISTS (SELECT 1 FROM information_schema.views WHERE table_name = 'migration_changelog_list_view') 
    THEN
'       CREATE VIEW `migration_changelog_list_view` 
        AS
        SELECT 
            id AS "ID",
            name AS "Name",
            (CASE status WHEN 0 THEN \'NotRan\' WHEN 1 THEN \'Successful\' WHEN 2 THEN \'Failed\' END) AS "StatusName",
            date_performed AS "DatePerformed",
            execution_duration AS "ExecutionDuration"
        FROM
            migration_changelog;'
    ELSE 
'       SELECT 1'
    END;

PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;