create table migration_changelog (
    id                  integer                     not null    primary key,
    name                text                        not null,
    status              integer                     not null,
    execution_duration  integer                     not null,
    date_performed      text                        not null    default (datetime('now'))
);

create unique index "ux_migration_changelog_name"               on migration_changelog ( name );
create        index "ix_migration_changelog_date_performed"     on migration_changelog ( date_performed );
create        index "ix_migration_changelog_execution_duration" on migration_changelog ( execution_duration );

create table migration_changelog_lock (
    id                  integer                     not null    primary key,
    name                text                        not null,
    date_created        text                        not null    default (datetime('now'))
);

create unique index "ux_migration_changelog_lock_name"          on migration_changelog_lock ( name );