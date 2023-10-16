using Benner.Tecnologia.Business;
using Benner.Tecnologia.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BSM.Tecnologia.Commom.Parametros
{
    /// <summary>
    /// Interface para a tabela K9_PARAMETROS
    /// </summary>
    public partial interface IParametros : IEntityBase
    {

        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        Benner.Tecnologia.Common.IEntityBase CampoInstance
        {
            get;
            set;
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        Handle CampoHandle
        {
            get;
            set;
        }

        /// <summary>
        /// Codigo (CODIGO.)
        /// Opcional = N, Invisível = False, Tamanho = 200
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        string Codigo
        {
            get;
            set;
        }

        /// <summary>
        /// Valor (INTEIRO.)
        /// Opcional = S, Invisível = False, Valor Mínimo = , Valor Máximo = , Tipo do Builder = Inteiro
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        System.Nullable<long> Inteiro
        {
            get;
            set;
        }

        /// <summary>
        /// Logico (LOGICO.)
        /// Opcional = S, Invisível = False, Default = False
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        System.Nullable<bool> Logico
        {
            get;
            set;
        }

        /// <summary>
        /// Descrição (DESCRICAO.)
        /// Opcional = N, Invisível = False, Tamanho = 500
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        string Descricao
        {
            get;
            set;
        }

        /// <summary>
        /// Numerico (NUMERICO.)
        /// Opcional = S, Invisível = False, Valor Mínimo = , Valor Máximo = , Tipo no Builder = Número
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        System.Nullable<decimal> Numerico
        {
            get;
            set;
        }

        /// <summary>
        /// Texto (TEXTO.)
        /// Opcional = S, Invisível = False, Tamanho = 500
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        string Texto
        {
            get;
            set;
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        Benner.Tecnologia.Common.IEntityBase TabelaInstance
        {
            get;
            set;
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        Handle TabelaHandle
        {
            get;
            set;
        }

        /// <summary>
        /// Tipo (TABTIPOPARAMETRO.)
        /// Opcional = S, Invisível = False
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        ParametrosTipoTabItens Tipo
        {
            get;
            set;
        }

        /// <summary>
        /// Valor (VALOR.)
        /// Opcional = S, Invisível = False, Tamanho = 50
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        string Valor
        {
            get;
            set;
        }

        /// <summary>
        /// Valorhandle (VALORHANDLE.)
        /// Opcional = S, Invisível = True, Tamanho = 100
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        string Valorhandle
        {
            get;
            set;
        }

        /// <summary>
        /// Senha (SENHA.)
        /// Opcional = S, Invisível = False, Tamanho = 50
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        string Senha
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Interface para o DAO para a tabela K9_PARAMETROS
    /// </summary>
    public partial interface IParametrosDao : IBusinessEntityDao<IParametros>
    {
    }

    /// <summary>
    /// DAO para a tabela K9_PARAMETROS
    /// </summary>
    public partial class ParametrosDao : BusinessEntityDao<Parametros, IParametros>, IParametrosDao
    {

        public static ParametrosDao CreateInstance()
        {
            return CreateInstance<ParametrosDao>();
        }
    }

    /// <summary>
    /// Esta classe contém os itens do campo TABTIPOPARAMETRO.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
    public class ParametrosTipoTabItens : TabItems<ParametrosTipoTabItens>
    {

        /// <summary>
        /// Valor = 1001, Item = Inteiro.
        /// </summary>
        public static ParametrosTipoTabItens ItemInteiro;

        /// <summary>
        /// Valor = 1002, Item = Numerico.
        /// </summary>
        public static ParametrosTipoTabItens ItemNumerico;

        /// <summary>
        /// Valor = 1003, Item = Logico.
        /// </summary>
        public static ParametrosTipoTabItens ItemLogico;

        /// <summary>
        /// Valor = 1004, Item = Texto.
        /// </summary>
        public static ParametrosTipoTabItens ItemTexto;

        /// <summary>
        /// Valor = 1005, Item = Senha.
        /// </summary>
        public static ParametrosTipoTabItens ItemSenha;

        /// <summary>
        /// Valor = 1006, Item = Tabela.
        /// </summary>
        public static ParametrosTipoTabItens ItemTabela;

        public static implicit operator ParametrosTipoTabItens(int index)
        {
            return GetByIndex(index);
        }

        public static implicit operator int(ParametrosTipoTabItens item)
        {
            return item.Index;
        }

        static ParametrosTipoTabItens()
        {
            ItemInteiro = new ParametrosTipoTabItens { Index = 1001, Description = "Inteiro" };
            ItemNumerico = new ParametrosTipoTabItens { Index = 1002, Description = "Numerico" };
            ItemLogico = new ParametrosTipoTabItens { Index = 1003, Description = "Logico" };
            ItemTexto = new ParametrosTipoTabItens { Index = 1004, Description = "Texto" };
            ItemSenha = new ParametrosTipoTabItens { Index = 1005, Description = "Senha" };
            ItemTabela = new ParametrosTipoTabItens { Index = 1006, Description = "Tabela" };

            Items.Add(ItemInteiro);
            Items.Add(ItemNumerico);
            Items.Add(ItemLogico);
            Items.Add(ItemTexto);
            Items.Add(ItemSenha);
            Items.Add(ItemTabela);

        }
    }

    /// <summary>
    /// Parametros
    /// </summary>
    [EntityDefinitionName("K9_PARAMETROS")]
    [DataContract(Namespace = "http://Benner.Tecnologia.Common.DataContracts/2007/09", Name = "EntityBase")]
    public partial class Parametros : BusinessEntity<Parametros>, IParametros
    {

        /// <summary>
        /// Possui constantes para retornarem o nome dos campos definidos no Builder para cada propriedade
        /// </summary>
        public static class FieldNames
        {
            public const string Campo = "CAMPO";
            public const string Codigo = "CODIGO";
            public const string Inteiro = "INTEIRO";
            public const string Logico = "LOGICO";
            public const string GUPOTABELA = "GUPOTABELA";
            public const string Descricao = "DESCRICAO";
            public const string Numerico = "NUMERICO";
            public const string Texto = "TEXTO";
            public const string Tabela = "TABELA";
            public const string Tipo = "TABTIPOPARAMETRO";
            public const string Valor = "VALOR";
            public const string Valorhandle = "VALORHANDLE";
            public const string Senha = "SENHA";
        }


        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public Benner.Tecnologia.Common.IEntityBase CampoInstance
        {
            get
            {
                if (Campo.Handle == null)
                {
                    return null;
                }
                return Campo.Instance;
            }
            set
            {
                if (value == null)
                {
                    Campo = null;
                    return;
                }
                Campo.Instance = (EntityBase)value;
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public Handle CampoHandle
        {
            get
            {
                return Campo.Handle;
            }
            set
            {
                Campo.Handle = value;
            }
        }

        /// <summary>
        /// Campo (CAMPO.)
        /// Opcional = S, Invisível = False, Pesquisar = Z_CAMPOS
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public Benner.Tecnologia.Common.EntityAssociation Campo
        {
            get
            {
                return Fields["CAMPO"] as Benner.Tecnologia.Common.EntityAssociation;
            }
            set
            {
                if (value == null)
                {
                    this.Campo.Handle = null;
                }
                else
                {
                    if (value.IsLoaded)
                    {
                        this.Campo.Instance = value.Instance;
                    }
                    else
                    {
                        this.Campo.Handle = value.Handle;
                    }
                }
            }
        }

        /// <summary>
        /// Codigo (CODIGO.)
        /// Opcional = N, Invisível = False, Tamanho = 200
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public string Codigo
        {
            get
            {
                return Fields["CODIGO"] as System.String;
            }
            set
            {
                Fields["CODIGO"] = value;
            }
        }

        /// <summary>
        /// Valor (INTEIRO.)
        /// Opcional = S, Invisível = False, Valor Mínimo = , Valor Máximo = , Tipo do Builder = Inteiro
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public System.Nullable<long> Inteiro
        {
            get
            {
                return Fields["INTEIRO"] as System.Nullable<System.Int64>;
            }
            set
            {
                Fields["INTEIRO"] = value;
            }
        }

        /// <summary>
        /// Logico (LOGICO.)
        /// Opcional = S, Invisível = False, Default = False
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public System.Nullable<bool> Logico
        {
            get
            {
                return Fields["LOGICO"] as System.Nullable<System.Boolean>;
            }
            set
            {
                Fields["LOGICO"] = value;
            }
        }

        /// <summary>
        /// Descrição (DESCRICAO.)
        /// Opcional = N, Invisível = False, Tamanho = 500
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public string Descricao
        {
            get
            {
                return Fields["DESCRICAO"] as System.String;
            }
            set
            {
                Fields["DESCRICAO"] = value;
            }
        }

        /// <summary>
        /// Numerico (NUMERICO.)
        /// Opcional = S, Invisível = False, Valor Mínimo = , Valor Máximo = , Tipo no Builder = Número
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public System.Nullable<decimal> Numerico
        {
            get
            {
                return Fields["NUMERICO"] as System.Nullable<System.Decimal>;
            }
            set
            {
                Fields["NUMERICO"] = value;
            }
        }

        /// <summary>
        /// Texto (TEXTO.)
        /// Opcional = S, Invisível = False, Tamanho = 500
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public string Texto
        {
            get
            {
                return Fields["TEXTO"] as System.String;
            }
            set
            {
                Fields["TEXTO"] = value;
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public Benner.Tecnologia.Common.IEntityBase TabelaInstance
        {
            get
            {
                if (Tabela.Handle == null)
                {
                    return null;
                }
                return Tabela.Instance;
            }
            set
            {
                if (value == null)
                {
                    Tabela = null;
                    return;
                }
                Tabela.Instance = (EntityBase)value;
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public Handle TabelaHandle
        {
            get
            {
                return Tabela.Handle;
            }
            set
            {
                Tabela.Handle = value;
            }
        }

        /// <summary>
        /// Tabela (TABELA.)
        /// Opcional = S, Invisível = False, Pesquisar = Z_TABELAS
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public Benner.Tecnologia.Common.EntityAssociation Tabela
        {
            get
            {
                return Fields["TABELA"] as Benner.Tecnologia.Common.EntityAssociation;
            }
            set
            {
                if (value == null)
                {
                    this.Tabela.Handle = null;
                }
                else
                {
                    if (value.IsLoaded)
                    {
                        this.Tabela.Instance = value.Instance;
                    }
                    else
                    {
                        this.Tabela.Handle = value.Handle;
                    }
                }
            }
        }

        /// <summary>
        /// Tipo (TABTIPOPARAMETRO.)
        /// Opcional = S, Invisível = False
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public ParametrosTipoTabItens Tipo
        {
            get
            {
                TabItem tabItem = Fields["TABTIPOPARAMETRO"] as TabItem;
                if (tabItem != null)
                    return new ParametrosTipoTabItens { Index = tabItem.Value, Description = tabItem.Text };
                return null;
            }
            set
            {
                if (value != null)
                    Fields["TABTIPOPARAMETRO"] = new TabItem(value.Index, value.Description);
                else
                    Fields["TABTIPOPARAMETRO"] = null;
            }
        }

        /// <summary>
        /// Valor (VALOR.)
        /// Opcional = S, Invisível = False, Tamanho = 50
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public string Valor
        {
            get
            {
                return Fields["VALOR"] as System.String;
            }
            set
            {
                Fields["VALOR"] = value;
            }
        }

        /// <summary>
        /// Valorhandle (VALORHANDLE.)
        /// Opcional = S, Invisível = True, Tamanho = 100
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public string Valorhandle
        {
            get
            {
                return Fields["VALORHANDLE"] as System.String;
            }
            set
            {
                Fields["VALORHANDLE"] = value;
            }
        }

        /// <summary>
        /// Senha (SENHA.)
        /// Opcional = S, Invisível = False, Tamanho = 50
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("BEF Code Generator", "20.1.1.971")]
        public string Senha
        {
            get
            {
                return Fields["SENHA"] as System.String;
            }
            set
            {
                Fields["SENHA"] = value;
            }
        }
    }
}
