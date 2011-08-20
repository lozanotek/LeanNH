namespace LeanNH {
    using System;
    using log4net;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Repository.Hierarchy;

    public class Logger {
        static Logger() {
            LoggerNames = new[] { 
                                    "NHibernate.Transaction.AdoTransaction", 
                                    "NHibernate.SQL", 
                                    "NHibernate.Impl.SessionImpl", 
                                    "NHibernate.Impl.StatelessSessionImpl", 
                                    "NHibernate.Impl.AbstractSessionImpl", 
                                    "NHibernate.Event.Default.DefaultInitializeCollectionEventListener", 
                                    "NHibernate.Event.Default.DefaultLoadEventListener", 
                                    "NHibernate.Cache.StandardQueryCache", 
                                    "NHibernate.Persister.Entity.AbstractEntityPersister", 
                                    "NHibernate.Loader.Loader", 
                                    "NHibernate.AdoNet.AbstractBatcher", 
                                    "NHibernate.Tool.hbm2ddl.SchemaExport", 
                                    "NHibernate.Tool.hbm2ddl.SchemaUpdate", 
                                    "NHibernate.Search.Query.FullTextQueryImpl" };
        }

        public static string[] LoggerNames { get; set; }

        public virtual void Supress() {
            Hierarchy repository = (Hierarchy)LogManager.GetRepository();
            StubAppender profilerAppender = new StubAppender();

            repository.Clear();

            log4net.Repository.Hierarchy.Logger logger;

            foreach (string str in LoggerNames) {
                logger = (log4net.Repository.Hierarchy.Logger)repository.GetLogger(str);
                logger.Level = Level.Fatal;
                logger.Additivity = false;
                AddAppender(profilerAppender, logger);
            }

            logger = (log4net.Repository.Hierarchy.Logger)repository.GetLogger("NHibernate");

            if ((logger.Level == null) || (logger.Level.Value > Level.Warn.Value)) {
                logger.Level = Level.Fatal;
            }

            AddAppender(profilerAppender, logger);
            profilerAppender.ActivateOptions();
            repository.Configured = true;
        }

        protected virtual void AddAppender(IAppender profilerAppender, IAppenderAttachable logger) {
            IAppender appender;
            do {
                appender = logger.GetAppender(profilerAppender.Name);
                if (appender != null) {
                    try {
                        logger.RemoveAppender(appender);
                    }
                    catch (Exception) { }
                }
            }
            while (appender != null);
            
            logger.AddAppender(profilerAppender);
        }
    }
}