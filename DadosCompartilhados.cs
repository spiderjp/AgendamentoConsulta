using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agendamento
{
    public static class DadosCompartilhados
    {
        private static int myId;
        public static int getId() { return myId; }

        public static void setId(int id) { myId = id; }
    }
}
