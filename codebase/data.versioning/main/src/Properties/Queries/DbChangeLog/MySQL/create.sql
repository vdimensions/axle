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

