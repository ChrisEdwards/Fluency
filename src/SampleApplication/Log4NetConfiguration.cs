using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;

//using System.Linq;


namespace SampleApplication
{
	public class Log4NetConfiguration
	{
		/// <summary>
		/// Configures Log4Net to write to the file C:\Application.log
		/// 
		/// After configuring, for each class you want to log from, add the following property to it:
		///    private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
		/// 
		/// Then to call the log, you can use:
		///    log.Debug( "your message to log" );
		/// 
		/// There are messages other than debug that you can use. Debug should only be used for debugging info. Use the
		/// correct message level for each log message.
		/// </summary>
		public static void Configure()
		{
			// Specify the format of each log entry.
			var layout = new PatternLayout
			             	{
			             			ConversionPattern = "%date [%thread] %-5level %logger - %message%newline",
			             			Header = "======= START LOGGING SESSION =======\n",
			             			Footer = "======== END LOGGING SESSION ========\n"
			             	};
			layout.ActivateOptions();

			// Specify the log file properties.
			var fileAppender = new RollingFileAppender
			                   	{
			                   			Layout = layout,
			                   			File = @"Application.log",
			                   			AppendToFile = true,
			                   			RollingStyle = RollingFileAppender.RollingMode.Date
			                   	};
			fileAppender.ActivateOptions();

			// Finalize this configuration.
			BasicConfigurator.Configure( fileAppender );
		}


		#region Helper Functions (Currently Not Used, But Are Useful To Keep)

		/// <summary>
		/// Set the level for a named logger.
		/// </summary>
		/// <param name="loggerName">Name of the logger.</param>
		/// <param name="levelName">Name of the level.</param>
		public static void SetLevel( string loggerName, string levelName )
		{
			ILog log = LogManager.GetLogger( loggerName );
			var l = (Logger)log.Logger;

			l.Level = l.Hierarchy.LevelMap[levelName];
		}


		/// <summary>
		/// Add an appender to a logger.
		/// </summary>
		/// <param name="loggerName">Name of the logger.</param>
		/// <param name="appender">The appender.</param>
		public static void AddAppender( string loggerName, IAppender appender )
		{
			ILog log = LogManager.GetLogger( loggerName );
			var l = (Logger)log.Logger;

			l.AddAppender( appender );
		}


		/// <summary>
		/// Find a named appender already attached to a logger.
		/// </summary>
		/// <param name="appenderName">Name of the appender.</param>
		/// <returns></returns>
		public static IAppender FindAppender( string appenderName )
		{
			// Commented code here can be used in place of following code, but needs to be tested first.
//            IAppender[] appenders = LogManager.GetRepository().GetAppenders();
//            return appenders.FirstOrDefault( appender => appender.Name == appenderName );

			foreach ( IAppender appender in LogManager.GetRepository().GetAppenders() )
			{
				if ( appender.Name == appenderName )
					return appender;
			}
			return null;
		}


		/// <summary>
		/// Create a new file appender.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public static IAppender CreateFileAppender( string name, string fileName )
		{
			var layout = new PatternLayout
			             	{
			             			ConversionPattern = "%d [%t] %-5p %c [%x] - %m%n"
			             	};
			layout.ActivateOptions();

			var appender = new FileAppender
			               	{
			               			Name = name,
			               			File = fileName,
			               			AppendToFile = true,
			               			Layout = layout
			               	};
			appender.ActivateOptions();

			return appender;
		}

		#endregion
	}
}