-- Estructura de la Base de Datos

-- Entidad Principal (Empresa)
CREATE TABLE empresa (
  id SERIAL PRIMARY KEY,
  razon_social VARCHAR(255) NOT NULL,
  nombre_comercial VARCHAR(255),
  ruc VARCHAR(20) UNIQUE NOT NULL,
  tipo_empresa VARCHAR(50) CHECK (tipo_empresa IN (
    'Sociedad Anónima (SA)', 'Sociedad por Acciones Simplificada (SAS)',
    'Compañía de Responsabilidad Limitada (Cía. Ltda.)',
    'Compañía en Nombre Colectivo', 'Compañía en Comandita Simple',
    'Compañía en Comandita por Acciones'
  )) NOT NULL,
  sector VARCHAR(100),
  direccion TEXT NOT NULL,
  ciudad VARCHAR(100),
  pais VARCHAR(100) NOT NULL,
  telefono VARCHAR(20),
  email VARCHAR(255) UNIQUE NOT NULL,
  sitio_web VARCHAR(255),
  logo_url VARCHAR(255),
  fecha_fundacion DATE,
  estado VARCHAR(50) CHECK (estado IN ('Activo', 'Inactivo')) DEFAULT 'Activo',
  fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  notas TEXT,
  representante_legal_id INTEGER UNIQUE REFERENCES empleado(id) ON DELETE SET NULL
);

-- Gestión de Clientes
CREATE TABLE clientes (
  id SERIAL PRIMARY KEY,
  razon_social VARCHAR(255) NOT NULL,
  nombre_comercial VARCHAR(255),
  ruc VARCHAR(20) UNIQUE NOT NULL,
  tipo_empresa VARCHAR(50) CHECK (tipo_empresa IN (
    'Sociedad Anónima (SA)', 'Sociedad por Acciones Simplificada (SAS)',
    'Compañía de Responsabilidad Limitada (Cía. Ltda.)',
    'Compañía en Nombre Colectivo', 'Compañía en Comandita Simple',
    'Compañía en Comandita por Acciones'
  )) NOT NULL,
  sector VARCHAR(100),
  direccion TEXT NOT NULL,
  ciudad VARCHAR(100),
  pais VARCHAR(100) NOT NULL,
  sitio_web VARCHAR(255),
  fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  estado VARCHAR(50) CHECK (estado IN ('Activo', 'Inactivo', 'Suspendido')) DEFAULT 'Activo',
  limite_credito DECIMAL(15,2) DEFAULT 0.00,
  notas TEXT
);

CREATE TABLE contactos_clientes (
  id SERIAL PRIMARY KEY,
  cliente_id INTEGER REFERENCES clientes(id) ON DELETE CASCADE,
  nombre VARCHAR(255) NOT NULL,
  cargo VARCHAR(100),
  telefono VARCHAR(20),
  email VARCHAR(255) NOT NULL,
  notas TEXT
);

-- Gestión de Empleados
CREATE TABLE empleado (
  id SERIAL PRIMARY KEY,
  primer_nombre VARCHAR(100) NOT NULL,
  segundo_nombre VARCHAR(100),
  apellido_paterno VARCHAR(100) NOT NULL,
  apellido_materno VARCHAR(100),
  tipo_documento VARCHAR(50) CHECK (tipo_documento IN ('Cédula', 'Pasaporte', 'Otro')) NOT NULL,
  numero_identificacion VARCHAR(20) UNIQUE NOT NULL,
  email_personal VARCHAR(255) UNIQUE NOT NULL,
  email_corporativo VARCHAR(255) UNIQUE,
  telefono VARCHAR(20),
  direccion TEXT,
  fecha_nacimiento DATE,
  genero VARCHAR(20) CHECK (genero IN ('Masculino', 'Femenino', 'Otro')),
  estado VARCHAR(50) CHECK (estado IN ('Activo', 'Inactivo', 'Suspendido', 'Eliminado')) DEFAULT 'Activo',
  fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE informacion_laboral_empleado (
  id SERIAL PRIMARY KEY,
  empleado_id INTEGER REFERENCES empleado(id) ON DELETE CASCADE,
  departamento_id INTEGER REFERENCES departamentos(id) ON DELETE SET NULL,
  cargo_id INTEGER REFERENCES cargos(id) ON DELETE SET NULL,
  fecha_ingreso DATE NOT NULL CHECK (fecha_ingreso <= CURRENT_DATE),
  fecha_salida DATE CHECK (fecha_salida IS NULL OR fecha_salida >= fecha_ingreso),
  salario DECIMAL(10,2),
  tipo_contrato VARCHAR(50) CHECK (tipo_contrato IN (
    'Indefinido', 'Temporal', 'Pasante', 'Contrato por obra', 'Freelance'
  )),
  supervisor_interno_id INTEGER REFERENCES empleado(id) ON DELETE SET NULL,
  supervisor_externo_id INTEGER REFERENCES contactos_clientes(id) ON DELETE SET NULL,
  notas TEXT
);

CREATE TABLE empleado_cliente (
  id SERIAL PRIMARY KEY,
  empleado_id INTEGER REFERENCES empleado(id) ON DELETE CASCADE,
  cliente_id INTEGER REFERENCES clientes(id) ON DELETE CASCADE,
  fecha_asignacion DATE DEFAULT CURRENT_DATE,
  fecha_fin DATE,
  estado VARCHAR(20) CHECK (estado IN ('Activo', 'Finalizado', 'Pendiente')) DEFAULT 'Activo'
);

-- Estrctura de la Empresa
CREATE TABLE departamentos (
  id SERIAL PRIMARY KEY,
  nombre VARCHAR(100) UNIQUE NOT NULL,
  descripcion TEXT
);

CREATE TABLE cargos (
  id SERIAL PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL,
  descripcion TEXT,
  departamento_id INTEGER REFERENCES departamentos(id) ON DELETE SET NULL,
  UNIQUE(nombre)
);

-- Usuarios del sistema y seguridad
CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  empleado_id INTEGER UNIQUE REFERENCES empleado(id) ON DELETE CASCADE,
  numero_identificacion VARCHAR(20) UNIQUE NOT NULL,
  usuario VARCHAR(255) UNIQUE NOT NULL,
  password_hash TEXT NOT NULL,
  intentos_fallidos INTEGER DEFAULT 0,
  bloqueado BOOLEAN DEFAULT FALSE,
  estado VARCHAR(20) CHECK (estado IN ('Activo', 'Inactivo', 'Bloqueado')) DEFAULT 'Activo',
  fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  fecha_ultimo_login TIMESTAMP
);

CREATE TABLE codigos_verificacion (
  id SERIAL PRIMARY KEY,
  usuario_id INTEGER REFERENCES users(id) ON DELETE CASCADE,
  codigo VARCHAR(6) NOT NULL,
  tipo VARCHAR(20) CHECK (tipo IN ('Registro', 'Recuperacion')) NOT NULL,
  fecha_generacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  expiracion TIMESTAMP NOT NULL,
  usado BOOLEAN DEFAULT FALSE
);

CREATE TABLE historial_eventos_usuario (
  id SERIAL PRIMARY KEY,
  usuario_id INTEGER REFERENCES users(id) ON DELETE CASCADE,
  tipo_evento VARCHAR(30) CHECK (tipo_evento IN ('Sesion', 'IntentoLogin', 'Bloqueo', 'Recuperacion')) NOT NULL,
  fecha_evento TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  ip VARCHAR(45),
  navegador VARCHAR(255),
  exito BOOLEAN,
  razon TEXT,
  motivo TEXT,
  fecha_cambio TIMESTAMP
);

-- Roles, Permisos, Accesos
CREATE TABLE roles (
  id SERIAL PRIMARY KEY,
  nombre VARCHAR(50) UNIQUE NOT NULL,
  descripcion TEXT
);

CREATE TABLE users_roles (
  id SERIAL PRIMARY KEY,
  usuario_id INTEGER REFERENCES users(id) ON DELETE CASCADE,
  rol_id INTEGER REFERENCES roles(id) ON DELETE CASCADE,
  UNIQUE(usuario_id, rol_id)
);

CREATE TABLE permisos (
  id SERIAL PRIMARY KEY,
  modulo_id INTEGER REFERENCES modulos(id) ON DELETE CASCADE,
  accion VARCHAR(20) CHECK (accion IN ('ver', 'crear', 'editar', 'eliminar')) NOT NULL,
  descripcion TEXT,
  UNIQUE(modulo_id, accion)
);

CREATE TABLE roles_permisos (
  id SERIAL PRIMARY KEY,
  rol_id INTEGER REFERENCES roles(id) ON DELETE CASCADE,
  permiso_id INTEGER REFERENCES permisos(id) ON DELETE CASCADE,
  UNIQUE(rol_id, permiso_id)
);

CREATE TABLE users_permisos (
  id SERIAL PRIMARY KEY,
  usuario_id INTEGER REFERENCES users(id) ON DELETE CASCADE,
  permiso_id INTEGER REFERENCES permisos(id) ON DELETE CASCADE,
  UNIQUE(usuario_id, permiso_id)
);

-- Módulos y Submódulos
CREATE TABLE modulos (
  id SERIAL PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL,
  descripcion TEXT,
  padre_id INTEGER REFERENCES modulos(id) ON DELETE CASCADE, -- para submódulos
  orden INTEGER DEFAULT 0,
  icono VARCHAR(100),
  UNIQUE(nombre, padre_id)
);

CREATE TABLE modulos_activados (
  id SERIAL PRIMARY KEY,
  modulo_id INTEGER REFERENCES modulos(id) ON DELETE CASCADE,
  activo BOOLEAN DEFAULT TRUE,
  fecha_activacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  UNIQUE(modulo_id)
);