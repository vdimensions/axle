CREATE TABLE [migration_changelog]
(
  [id]                  INT             NOT NULL    IDENTITY(1,1),
  [name]                VARCHAR(255)    NOT NULL,
  [status]              TINYINT         NOT NULL,
  [execution_duration]  BIGINT          NOT NULL,
  [date_performed]      DATETIME        NOT NULL
);

ALTER TABLE [migration_changelog] 
  ADD CONSTRAINT [migration_changelog_pk] 
      PRIMARY KEY CLUSTERED ( [id] )
      WITH 
      (
        PAD_INDEX              = OFF, 
        STATISTICS_NORECOMPUTE = OFF, 
        SORT_IN_TEMPDB         = OFF, 
        IGNORE_DUP_KEY         = OFF, 
        ONLINE                 = OFF, 
        ALLOW_ROW_LOCKS        = ON, 
        ALLOW_PAGE_LOCKS       = ON
      );

CREATE UNIQUE NONCLUSTERED INDEX [migration_changelog_ux01] ON [migration_changelog] ( [name] ASC );
CREATE        NONCLUSTERED INDEX [migration_changelog_ix01] ON [migration_changelog] ( [date_performed] ASC );
CREATE        NONCLUSTERED INDEX [migration_changelog_ix02] ON [migration_changelog] ( [execution_duration] ASC );

CREATE TABLE [migration_changelog_lock]
(
  [id]                  INT             NOT NULL,
  [name]                VARCHAR(255)    NOT NULL,
  [date_created]        DATETIME        NOT NULL    DEFAULT GETUTCDATE()
);

ALTER TABLE [migration_changelog_lock] 
  ADD CONSTRAINT [migration_changelog_lock_pk] 
      PRIMARY KEY CLUSTERED ( [id] )
      WITH 
      (
        PAD_INDEX              = OFF, 
        STATISTICS_NORECOMPUTE = OFF, 
        SORT_IN_TEMPDB         = OFF, 
        IGNORE_DUP_KEY         = OFF, 
        ONLINE                 = OFF, 
        ALLOW_ROW_LOCKS        = ON, 
        ALLOW_PAGE_LOCKS       = ON
      );

CREATE UNIQUE INDEX [migration_changelog_lock_ux01] ON [migration_changelog_lock] ( [name] ASC );