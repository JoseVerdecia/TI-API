using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TI_API.Infraestucture.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Evaluacion = table.Column<string>(type: "text", nullable: false, defaultValue: "NoEvaluado")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    JefeDeAreaId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AreaAsignadaId = table.Column<int>(type: "integer", nullable: true),
                    ProcesoAsignadoId = table.Column<int>(type: "integer", nullable: true),
                    RefreshToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Areas_AreaAsignadaId",
                        column: x => x.AreaAsignadaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Evaluacion = table.Column<string>(type: "text", nullable: false, defaultValue: "NoEvaluado"),
                    JefeDeProcesoId = table.Column<Guid>(type: "uuid", nullable: true),
                    ObjetivoModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procesos_AspNetUsers_JefeDeProcesoId",
                        column: x => x.JefeDeProcesoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Procesos_Objetivos_ObjetivoModelId",
                        column: x => x.ObjetivoModelId,
                        principalTable: "Objetivos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Indicadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Comentario = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Origen = table.Column<string>(type: "text", nullable: false),
                    MetaCumplir = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DecimalMetaCumplir = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsMetaCumplirPorcentage = table.Column<bool>(type: "boolean", nullable: false),
                    MetaReal = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DecimalMetaReal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsMetaRealPorcentage = table.Column<bool>(type: "boolean", nullable: false),
                    Evaluacion = table.Column<string>(type: "text", nullable: false),
                    ProcesoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indicadores_Procesos_ProcesoId",
                        column: x => x.ProcesoId,
                        principalTable: "Procesos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndicadoresDeArea",
                columns: table => new
                {
                    IndicadorId = table.Column<int>(type: "integer", nullable: false),
                    AreaId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    MetaCumplirArea = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DecimalMetaCumplirArea = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsMetaCumplirAreaPorcentage = table.Column<bool>(type: "boolean", nullable: false),
                    MetaRealArea = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DecimalMetaRealArea = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsMetaRealAreaPorcentage = table.Column<bool>(type: "boolean", nullable: false),
                    Comentario = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Evaluacion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadoresDeArea", x => new { x.IndicadorId, x.AreaId });
                    table.ForeignKey(
                        name: "FK_IndicadoresDeArea_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicadoresDeArea_Indicadores_IndicadorId",
                        column: x => x.IndicadorId,
                        principalTable: "Indicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndicadoresDeObjetivo",
                columns: table => new
                {
                    IndicadorId = table.Column<int>(type: "integer", nullable: false),
                    ObjetivoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadoresDeObjetivo", x => new { x.IndicadorId, x.ObjetivoId });
                    table.ForeignKey(
                        name: "FK_IndicadoresDeObjetivo_Indicadores_IndicadorId",
                        column: x => x.IndicadorId,
                        principalTable: "Indicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicadoresDeObjetivo_Objetivos_ObjetivoId",
                        column: x => x.ObjetivoId,
                        principalTable: "Objetivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Mensaje = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    MensajeRechazo = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    MetaCumplirPropuesta = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true, defaultValue: "Pendiente"),
                    RemitenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinatarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaId = table.Column<int>(type: "integer", nullable: false),
                    IndicadorDeAreaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificaciones_AspNetUsers_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificaciones_AspNetUsers_RemitenteId",
                        column: x => x.RemitenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificaciones_IndicadoresDeArea_IndicadorDeAreaId_AreaId",
                        columns: x => new { x.IndicadorDeAreaId, x.AreaId },
                        principalTable: "IndicadoresDeArea",
                        principalColumns: new[] { "IndicadorId", "AreaId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "JefeDeAreaId", "Nombre", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "FACIM", "Facultad" },
                    { 2, null, "FACUP", "Facultad" },
                    { 3, null, "Baguanos", "Municipio" },
                    { 4, null, "Gibara", "Municipio" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0d3087c0-ffe7-4482-921f-588fbf057d8b"), null, "Admin", "ADMIN" },
                    { new Guid("1f0c337b-e8fe-43ed-b5e9-b49139d8f7bf"), null, "UsuarioNormal", "USUARIONORMAL" },
                    { new Guid("50ea312a-b027-4b57-9b9c-fd006e712213"), null, "JefeProceso", "JEFEPROCESO" },
                    { new Guid("765e0479-8871-4e35-a215-3e41670c5ed5"), null, "JefeArea", "JEFEAREA" }
                });

            migrationBuilder.InsertData(
                table: "Objetivos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Formar profesionales integrales, competentes, con espíritu innovador y firmeza político ideológica" },
                    { 2, "Lograr la preparación y el completamiento del claustro y de los cuadros" },
                    { 3, "Fortalecer el vínculo de la Educación Superior con las empresas" },
                    { 4, "Impactar al desarrollo científico y tecnológico" },
                    { 5, "Perfeccionar la preparación y superación de los cuadros y reservas" },
                    { 6, "Potenciar la relación universidad-sociedad" },
                    { 7, "Garantizar la transformación digital de las Universidades" },
                    { 8, "Gestionar los recursos humanos, materiales y financieros" },
                    { 9, "Asegurar la calidad de la Educación Superior Cubana" }
                });

            migrationBuilder.InsertData(
                table: "Procesos",
                columns: new[] { "Id", "JefeDeProcesoId", "Nombre", "ObjetivoModelId" },
                values: new object[,]
                {
                    { 1, null, "Pregrado", null },
                    { 2, null, "Posgrado", null },
                    { 3, null, "Ciencia, Tecnología e Innovación", null },
                    { 4, null, "Extensión Universitaria", null },
                    { 5, null, "Recursos Humanos", null },
                    { 6, null, "Información, Comunicación e Informatización", null },
                    { 7, null, "Internacionalización", null },
                    { 8, null, "Aseguramiento Material y Financiero", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_JefeDeAreaId",
                table: "Areas",
                column: "JefeDeAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Nombre_Tipo",
                table: "Areas",
                columns: new[] { "Nombre", "Tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Tipo",
                table: "Areas",
                column: "Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AreaAsignadaId",
                table: "AspNetUsers",
                column: "AreaAsignadaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Nombre",
                table: "AspNetUsers",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProcesoAsignadoId",
                table: "AspNetUsers",
                column: "ProcesoAsignadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RefreshToken",
                table: "AspNetUsers",
                column: "RefreshToken");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_Evaluacion",
                table: "Indicadores",
                column: "Evaluacion");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_Nombre",
                table: "Indicadores",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_ProcesoId",
                table: "Indicadores",
                column: "ProcesoId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_Tipo",
                table: "Indicadores",
                column: "Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadoresDeArea_AreaId",
                table: "IndicadoresDeArea",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadoresDeArea_Evaluacion",
                table: "IndicadoresDeArea",
                column: "Evaluacion");

            migrationBuilder.CreateIndex(
                name: "IX_IndicadoresDeObjetivo_ObjetivoId",
                table: "IndicadoresDeObjetivo",
                column: "ObjetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_AreaId",
                table: "Notificaciones",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_DestinatarioId",
                table: "Notificaciones",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_Estado",
                table: "Notificaciones",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_FechaCreacion",
                table: "Notificaciones",
                column: "FechaCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_IndicadorDeAreaId_AreaId",
                table: "Notificaciones",
                columns: new[] { "IndicadorDeAreaId", "AreaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_RemitenteId",
                table: "Notificaciones",
                column: "RemitenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_Evaluacion",
                table: "Objetivos",
                column: "Evaluacion");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_Nombre",
                table: "Objetivos",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Procesos_Evaluacion",
                table: "Procesos",
                column: "Evaluacion");

            migrationBuilder.CreateIndex(
                name: "IX_Procesos_JefeDeProcesoId",
                table: "Procesos",
                column: "JefeDeProcesoId");

            migrationBuilder.CreateIndex(
                name: "IX_Procesos_Nombre",
                table: "Procesos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procesos_ObjetivoModelId",
                table: "Procesos",
                column: "ObjetivoModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_AspNetUsers_JefeDeAreaId",
                table: "Areas",
                column: "JefeDeAreaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Procesos_ProcesoAsignadoId",
                table: "AspNetUsers",
                column: "ProcesoAsignadoId",
                principalTable: "Procesos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_AspNetUsers_JefeDeAreaId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Procesos_AspNetUsers_JefeDeProcesoId",
                table: "Procesos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "IndicadoresDeObjetivo");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "IndicadoresDeArea");

            migrationBuilder.DropTable(
                name: "Indicadores");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Procesos");

            migrationBuilder.DropTable(
                name: "Objetivos");
        }
    }
}
