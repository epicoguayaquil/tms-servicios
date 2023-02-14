namespace ec.gob.mimg.tms.api.Enums
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
        HABILITADO
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
        NO_CUMPLE
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
