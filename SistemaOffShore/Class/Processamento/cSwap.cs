using System;

namespace SistemaOffShore.Class
{
    public class cSwap
    {
        public string CLCLI_CD { get; set; }
        public DateTime DT { get; set; }
        public string SWCAD_CD { get; set; }
        public decimal QT { get; set; }
        public decimal VL_PASSIVO { get; set; }
        public decimal VL_ATIVO { get; set; }
        public decimal VL_APROP_LIQ { get; set; }
        public int ID_ARQ { get; set; }

        public cSwap(string _CLCLI_CD,
                    DateTime _DT,
                    string _SWCAD_CD,
                    decimal _QT,
                    decimal _VL_PASSIVO,
                    decimal _VL_ATIVO,
                    decimal _VL_APROP_LIQ,
                    int _ID_ARQ)
        {
            CLCLI_CD = _CLCLI_CD;
            DT = _DT;
            SWCAD_CD = _SWCAD_CD;
            QT = _QT;
            VL_PASSIVO = _VL_PASSIVO;
            VL_ATIVO = _VL_ATIVO;
            VL_APROP_LIQ = _VL_APROP_LIQ;
            ID_ARQ = _ID_ARQ;
        }

        public cSwap() { }
    }
}
