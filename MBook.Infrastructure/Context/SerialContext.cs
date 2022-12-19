using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBook.Infrastructure.Context
{
    public class SerialContext
    {
        public SerialPort SerialPort { get; private set; }
        private static SerialContext serialContext=null;

        /// <summary>
        /// construtor privado
        /// </summary>
        private SerialContext(SerialPort sp)
        {
            SerialPort = sp;
        }

        /// <summary>
        /// define o contexto
        /// </summary>
        /// <param name="sp"></param>
        public static  void SetContext(SerialPort sp)
        {
            if(serialContext==null)serialContext=new SerialContext(sp);
        }

        /// <summary>
        /// singleton instance
        /// </summary>
        public static SerialContext Instance
        {
            get { return serialContext; }
        }
    }
}
