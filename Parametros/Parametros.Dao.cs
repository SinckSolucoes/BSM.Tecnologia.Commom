using Benner.Tecnologia.Common.Security;
using Benner.Tecnologia.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSM.Tecnologia.Commom.Parametros.Parametros;

namespace BSM.Tecnologia.Commom.Parametros
{
    public partial interface IParametrosDao
    {
        string BuscaParametro(string parametro);

        IList<IParametros> BuscarParametros(string parametroConsulta);
    }

    public partial class ParametrosDao : IParametrosDao
    {
        public string BuscaParametro(string parametro)
        {
            IParametros param = GetFirstOrDefault(x => x.Codigo == parametro);

            if (param is null)
            {
                return null;
            }
            else
            {
                switch (param.Tipo.Index)
                {
                    case 1001: return param.Inteiro.ToString();

                    case 1002: return param.Numerico.ToString();

                    case 1003: return param.Logico.ToString();

                    case 1004: return param.Texto;

                    case 1005: return Scramble.GetScrambled(param.Senha);
                    case 1006: return param.Valorhandle;

                    default: return null;
                }
            }
        }

        public IList<IParametros> BuscarParametros(string parametroConsulta)
        {
            //feito assim pois passar um parametro dentro do like retornou nada :(
            Criteria criterio = new Criteria("A.CODIGO LIKE '" + parametroConsulta + "'");
            criterio.CompanyFilterMode = CompanyFilterMode.None;

            return GetMany(criterio);
        }
    }
}
