-- Insertar empleados
-- Empleado 1: Admin
INSERT INTO empleado (
  primer_nombre, segundo_nombre, apellido_paterno, apellido_materno,
  tipo_documento, numero_identificacion,
  email_personal, email_corporativo,
  telefono, direccion, fecha_nacimiento, genero
) VALUES (
  'Carlos', 'Eduardo', 'Mora', 'Sánchez',
  'Cédula', '0912345678',
  'carlos.mora@gmail.com', 'carlos@empresa.com',
  '0999988776', 'Av. Principal 123', '1985-03-12', 'Masculino'
);

-- Empleado 2: Ejecutivo Comercial
INSERT INTO empleado (
  primer_nombre, segundo_nombre, apellido_paterno, apellido_materno,
  tipo_documento, numero_identificacion,
  email_personal, email_corporativo,
  telefono, direccion, fecha_nacimiento, genero
) VALUES (
  'Andrea', 'Mariela', 'Torres', 'Muñoz',
  'Cédula', '0923456789',
  'andrea.torres@gmail.com', 'andrea@empresa.com',
  '0988776655', 'Calle 100 y Av. Sur', '1992-11-25', 'Femenino'
);

-- Empleado 3: Gerente Comercial
INSERT INTO empleado (
  primer_nombre, segundo_nombre, apellido_paterno, apellido_materno,
  tipo_documento, numero_identificacion,
  email_personal, email_corporativo,
  telefono, direccion, fecha_nacimiento, genero
) VALUES (
  'Marcela', 'Gabriela', 'Ríos', 'Valencia',
  'Cédula', '0911223344',
  'marcela.rios@gmail.com', 'marcela@empresa.com',
  '0998877665', 'Av. del Valle 456', '1980-08-10', 'Femenino'
);

-- Empleado 4: Líder de Ventas
INSERT INTO empleado (
  primer_nombre, segundo_nombre, apellido_paterno, apellido_materno,
  tipo_documento, numero_identificacion,
  email_personal, email_corporativo,
  telefono, direccion, fecha_nacimiento, genero
) VALUES (
  'Luis', 'Antonio', 'Narváez', 'Paredes',
  'Cédula', '0933665599',
  'luis.narvaez@gmail.com', 'luis@empresa.com',
  '0988123456', 'Calle Norte 789', '1988-06-15', 'Masculino'
);

-- Insertar roles base del sistema
INSERT INTO roles (nombre, descripcion) VALUES
('Admin', 'Administrador del sistema con todos los permisos'),
('Gerente Comercial', 'Visualiza y gestiona toda la información del área comercial'),
('Líder de Ventas', 'Gestiona y supervisa a los ejecutivos de ventas asignados'),
('Ejecutivo Comercial', 'Gestiona sus propios clientes, oportunidades e interacciones');


-- Insert de usuarios
-- Usuario 1: Admin
INSERT INTO users (
  empleado_id, numero_identificacion, usuario, password_hash
) VALUES (
  1, '0912345678', 'cmora', '$2b$10$hashadmin1234567890hashadmin'
);

-- Usuario 2: Ejecutivo Comercial
INSERT INTO users (
  empleado_id, numero_identificacion, usuario, password_hash
) VALUES (
  2, '0923456789', 'atorres', '$2b$10$hashejecutivo9876543210hash'
);

-- Usuario 3: Gerente Comercial
INSERT INTO users (
  empleado_id, numero_identificacion, usuario, password_hash
) VALUES (
  3, '0911223344', 'mrios', '$2b$10$hashgerente1234567890'
);

-- Usuario 4: Líder de Ventas
INSERT INTO users (
  empleado_id, numero_identificacion, usuario, password_hash
) VALUES (
  4, '0933665599', 'lnarvaez', '$2b$10$hashlider9876543210'
);


-- Estructura de inserción de Módulos y Sub-Módulos
-- Módulos principales
INSERT INTO modulos (nombre, descripcion) VALUES
('Autenticación y Seguridad', 'Gestión de acceso y roles'),
('Ventas', 'Módulo de gestión comercial'),
('Reportes Módulo de Ventas', 'Análisis y KPIs de ventas');

-- Submódulos con relación jerárquica (padre_id)
-- NOTA: debes obtener el ID real de cada módulo principal primero

-- Supongamos:
-- id = 1 → Autenticación y Seguridad
-- id = 2 → Ventas
-- id = 3 → Reportes Módulo de Ventas

-- Submódulos para Autenticación y Seguridad
INSERT INTO modulos (nombre, descripcion, padre_id) VALUES
('Usuarios', 'Gestión de usuarios', 1),
('Roles y Permisos', 'Control de roles', 1),
('Historial de Accesos', 'Log de accesos', 1);


-- Submódulos para Reportes de Ventas
INSERT INTO modulos (nombre, descripcion, padre_id) VALUES
('Ventas', 'Reporte de Ventas',3),
('Oportunidades de Ventas', 'Oportunidades de Ventas', 3),
('Desempeño de ejecutivo', 'Desempeño de ejecutivo', 3),
('Interacciones', 'Desempeño de ejecutivo', 3),
('Proyectos Generados', 'Proyectos Generados', 3);

-- Submódulos para Ventas
INSERT INTO modulos (nombre, descripcion, padre_id) VALUES
('Clientes', 'Clientes asignados', 2),
('Oportunidades', 'Oportunidades de negocio', 2),
('Interacciones', 'Seguimiento comercial', 2);

-- Submódulos para Reportes de Ventas
INSERT INTO modulos (nombre, descripcion, padre_id) VALUES
('Ventas', 'Reporte de ventas', 3),
('Oportunidades de Ventas', 'Reporte de oportunidades', 3),
('Desempeño de ejecutivo', 'KPIs individuales', 3),
('Interacciones', 'Reporte de interacciones', 3),
('Proyectos Generados', 'Proyectos desde ventas', 3);


-- Asignación de rol a usuarios 
-- Admin
INSERT INTO users_roles (usuario_id, rol_id) VALUES (1, 1); -- Carlos

-- Ejecutivo Comercial
INSERT INTO users_roles (usuario_id, rol_id) VALUES (2, 4); -- Andrea

-- Gerente Comercial (rol_id = 2)
INSERT INTO users_roles (usuario_id, rol_id)
VALUES (3, 2);

-- Líder de Ventas (rol_id = 3)
INSERT INTO users_roles (usuario_id, rol_id)
VALUES (4, 3);