namespace ThesisManager.Core.Domain.Mappings {
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Helpers;
    using FluentNHibernate.Conventions.Inspections;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    using Spring.Data.NHibernate;

    /// <summary>
    ///     Session Factory für zur Nutzung der Fluent und/oder HBM Mappings
    /// </summary>
    // ReSharper disable UnusedMember.Global
    public class FluentSessionFactory : LocalSessionFactoryObject {
        private Configuration _configuration;

        /// <summary>
        ///     Auflistung der Assemblies, in denen Mapping Fluent/HBM Mapping Dateien vorkommen.
        ///     Wird über die Spring.Database.config.xml verwendet.
        /// </summary>
        // ReSharper disable UnusedMember.Global
        public new string[] MappingAssemblies { // ReSharper restore UnusedMember.Global
            get; set; }

        private IList<IConvention> Conventions {
            get {
                return new List<IConvention> {
                    DefaultAccess.CamelCaseField(CamelCasePrefix.Underscore), DefaultLazy.Never(), ForeignKey.EndsWith("_Id"),
                    Table.Is(x => "tbl" + x.EntityType.Name)
                };
            }
        }

        protected override void PostProcessConfiguration(Configuration config) {
            /* Basiskonfiguration durchführen */
            base.PostProcessConfiguration(config);

            /* HBM Exportpfad */
            //string schemaExportPath = Path.Combine(System.Environment.CurrentDirectory, ".");

            Fluently.Configure(config).Mappings(m => {
                                                    foreach (string assemblyName in MappingAssemblies) {
                                                        /* HBM Mappings der Konfiguration hinzufügen. */
                                                        m.HbmMappings.AddFromAssembly(Assembly.Load(assemblyName));

                                                        /* 
                                                         * Fluent Mappings und Konventionen der Konfiguration hinzufügen sowie als HBM exportieren.
                                                         * 
                                                         * Conventions s. https://github.com/jagregory/fluent-nhibernate/wiki/Conventions
                                                         * und https://github.com/jagregory/fluent-nhibernate/wiki/Available-conventions
                                                         * 
                                                         */
                                                        m.FluentMappings.AddFromAssembly(Assembly.Load(assemblyName))
                                                         .Conventions.Add(Conventions.ToArray()); //.ExportTo(schemaExportPath);
                                                    }
                                                }).BuildConfiguration();

            _configuration = config;

            string connectionString = DbProvider.ConnectionString;

            string filePath = connectionString.Replace("Data Source=", string.Empty).Replace(";Version=3;", string.Empty);

            if (!File.Exists(filePath)) {
                try {
                    SchemaExport schemaExport = new SchemaExport(config);
                    schemaExport.Create(false, true);
                } catch (HibernateException) {
                    throw new IOException(string.Format("Keine Schreibberechtigungen auf den Pfad {0}!", filePath));
                }
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}