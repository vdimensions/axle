CREATE TABLE IF NOT EXISTS migration_changelog (
  id                  SERIAL                       NOT NULL,
  name                VARCHAR(255)                 NOT NULL,
  status              SMALLINT                     NOT NULL,
  execution_duration  BIGINT                       NOT NULL,
  date_performed      TIMESTAMP WITHOUT TIME ZONE  NOT NULL
);

ALTER TABLE migration_changelog
ADD CONSTRAINT "migration_changelog_pk" ;

CREATE UNIQUE INDEX "migration_changelog_ux01" ON migration_changelog ( name ) ;
CREATE        INDEX "migration_changelog_ix01" ON migration_changelog ( date_performed ) ;
CREATE        INDEX "migration_changelog_ix02" ON migration_changelog ( execution_duration ) ;
