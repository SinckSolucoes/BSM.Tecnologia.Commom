using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Benner.Tecnologia.Business;
using Benner.Tecnologia.Common;
using Ninject;

namespace BSM.Tecnologia.Commom.Parametros
{
    /// <summary>
    /// Nome da Tabela: K9_PARAMETROS.
    /// Essa é uma classe parcial, os atributos, herança e propriedades estão definidos no arquivo Parametros.properties.cs
    /// </summary>
    public partial class Parametros
    {
        [Inject]
        public IParametrosDao parametrosDao { get; set; }

        protected override void Validating()
        {
            if (State == EntityState.Initialized && parametrosDao.Exists(x => x.Codigo == this.Codigo))
            {
                throw new BusinessException("Parametro já cadastrado!");
            }

            base.Validating();
        }

        protected override void Saving()
        {
            try
            {
                if (Tipo == ParametrosTipoTabItens.ItemTabela)
                {
                    EntityBase tabela = Entity.GetFirstOrDefault(EntityDefinition.GetByName("Z_TABELAS"), new Criteria("A.HANDLE = " + Tabela.Handle.Value));

                    EntityBase campo = Entity.GetFirstOrDefault(EntityDefinition.GetByName("Z_CAMPOS"), new Criteria("A.HANDLE = " + Campo.Handle.Value));
                    Query query = new Query("SELECT HANDLE FROM " + tabela.Fields["NOME"].ToString() + " WHERE " + campo.Fields["NOME"].ToString() + " = '" + Valor + "'");
                    var resultados = query.Execute();

                    foreach (EntityBase resultado in resultados)
                    {
                        Valorhandle = resultado.Fields["HANDLE"].ToString();
                    }
                }
            }
            catch
            {

            }

            base.Saving();
        }

    }
}
