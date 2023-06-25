﻿namespace ec.gob.mimg.tms.api.Enums
{
    enum EstadoEnum
    {
        ACTIVO,
        INACTIVO,
        EN_PROCESO
    }
    enum EstadoEstablecimientoEnum
    {
        INHABILITADO,
        HABILITADO,
        ABIERTO,
        CERRADO
    }
    enum EstadoRegistroEnum
    {
        NO_REGISTRADO,
        EN_PROCESO,
        REGISTRADO
    }
    enum EstadoObligacionEnum
    {
        CUMPLE,
        NO_CUMPLE,
        EN_EXCEPCION
    }
    enum TipoExigibilidadEnum
    {
        VENCIMIENTO,
        POR_MES
    }
    enum JerarquiaObligacion
    {
        EMPRESA,
        ESTABLECIMIENTO
    }

    enum TipoCaracteristica
    {
        FORMULARIO,
        GESTION
    }
}
