using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MossadAPI.Migrations
{
    /// <inheritdoc />
    public partial class letsHope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentLocation_Agent_AgentId",
                table: "AgentLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_Mission_Agent_AgentId",
                table: "Mission");

            migrationBuilder.DropForeignKey(
                name: "FK_Target_Mission_MissionId",
                table: "Target");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetLocation_Target_TargetId",
                table: "TargetLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetLocation",
                table: "TargetLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Target",
                table: "Target");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mission",
                table: "Mission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgentLocation",
                table: "AgentLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.RenameTable(
                name: "TargetLocation",
                newName: "TargetLocations");

            migrationBuilder.RenameTable(
                name: "Target",
                newName: "Targets");

            migrationBuilder.RenameTable(
                name: "Mission",
                newName: "Missions");

            migrationBuilder.RenameTable(
                name: "AgentLocation",
                newName: "AgentLocations");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agents");

            migrationBuilder.RenameIndex(
                name: "IX_TargetLocation_TargetId",
                table: "TargetLocations",
                newName: "IX_TargetLocations_TargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Target_MissionId",
                table: "Targets",
                newName: "IX_Targets_MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Mission_AgentId",
                table: "Missions",
                newName: "IX_Missions_AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_AgentLocation_AgentId",
                table: "AgentLocations",
                newName: "IX_AgentLocations_AgentId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Targets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "TotalTime",
                table: "Missions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetLocations",
                table: "TargetLocations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Targets",
                table: "Targets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Missions",
                table: "Missions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgentLocations",
                table: "AgentLocations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentLocations_Agents_AgentId",
                table: "AgentLocations",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Agents_AgentId",
                table: "Missions",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetLocations_Targets_TargetId",
                table: "TargetLocations",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Missions_MissionId",
                table: "Targets",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentLocations_Agents_AgentId",
                table: "AgentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Agents_AgentId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetLocations_Targets_TargetId",
                table: "TargetLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Missions_MissionId",
                table: "Targets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Targets",
                table: "Targets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetLocations",
                table: "TargetLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Missions",
                table: "Missions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgentLocations",
                table: "AgentLocations");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Targets");

            migrationBuilder.RenameTable(
                name: "Targets",
                newName: "Target");

            migrationBuilder.RenameTable(
                name: "TargetLocations",
                newName: "TargetLocation");

            migrationBuilder.RenameTable(
                name: "Missions",
                newName: "Mission");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Agent");

            migrationBuilder.RenameTable(
                name: "AgentLocations",
                newName: "AgentLocation");

            migrationBuilder.RenameIndex(
                name: "IX_Targets_MissionId",
                table: "Target",
                newName: "IX_Target_MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_TargetLocations_TargetId",
                table: "TargetLocation",
                newName: "IX_TargetLocation_TargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Missions_AgentId",
                table: "Mission",
                newName: "IX_Mission_AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_AgentLocations_AgentId",
                table: "AgentLocation",
                newName: "IX_AgentLocation_AgentId");

            migrationBuilder.AlterColumn<int>(
                name: "TotalTime",
                table: "Mission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Target",
                table: "Target",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetLocation",
                table: "TargetLocation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mission",
                table: "Mission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgentLocation",
                table: "AgentLocation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentLocation_Agent_AgentId",
                table: "AgentLocation",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_Agent_AgentId",
                table: "Mission",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Target_Mission_MissionId",
                table: "Target",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetLocation_Target_TargetId",
                table: "TargetLocation",
                column: "TargetId",
                principalTable: "Target",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
