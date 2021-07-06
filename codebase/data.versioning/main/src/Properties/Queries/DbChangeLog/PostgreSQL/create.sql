CREATE TABLE migration_changelog (
  id                  SERIAL                       NOT NULL,
  name                VARCHAR(255)                 NOT NULL,
  status              SMALLINT                     NOT NULL,
  execution_duration  BIGINT                       NOT NULL,
  date_performed      TIMESTAMP WITHOUT TIME ZONE  NOT NULL
);

ALTER TABLE migration_changelog
  ADD CONSTRAINT "migration_changelog_pk" PRIMARY KEY ( id );

CREATE UNIQUE INDEX "migration_changelog_ux01" ON migration_changelog ( name );
CREATE        INDEX "migration_changelog_ix01" ON migration_changelog ( date_performed );
CREATE        INDEX "migration_changelog_ix02" ON migration_changelog ( execution_duration );

CREATE TABLE migration_changelog_lock (
  id                  SERIAL                       NOT NULL,
  name                VARCHAR(255)                 NOT NULL,
  date_created        TIMESTAMPTZ                  NOT NULL DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE migration_changelog_lock
  ADD CONSTRAINT "migration_changelog_lock_pk"  PRIMARY KEY ( id );

CREATE UNIQUE INDEX "migration_changelog_lock_ux01" ON migration_changelog_lock ( name );