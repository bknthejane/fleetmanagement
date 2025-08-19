param(
    [Parameter(Mandatory=$false)]
    [string]$Namespace = "shesha.app.Domain.Migrations",
    
    [Parameter(Mandatory=$false)]
    [string]$OutputPath = "."
)

$timestamp = Get-Date -Format "yyyyMMddHHmmss"
$className = "M$timestamp"

# Create the migration content
$migrationContent = @"
using FluentMigrator;
using Shesha.FluentMigrator;
using System;

namespace $Namespace
{
    [Migration($timestamp)]
    public class $className : Migration
    {

        public override void Up()
        {
            throw new NotImplementedException();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
"@

$filename = "$className.cs"
$fullPath = Join-Path $OutputPath $filename
$migrationContent | Out-File -FilePath $fullPath -Encoding UTF8

Write-Host "Migration created successfully!" -ForegroundColor Green
Write-Host "File: $fullPath" -ForegroundColor Green
