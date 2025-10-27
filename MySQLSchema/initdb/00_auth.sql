-- DEV ONLY: atur autentikasi root ke mysql_native_password tanpa password
-- Catatan: Jangan gunakan ini di PROD.

-- Pastikan entry root@'%' ada (jika belum)
CREATE USER IF NOT EXISTS 'root'@'%' IDENTIFIED WITH mysql_native_password BY '';

ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY '';
ALTER USER 'root'@'%'         IDENTIFIED WITH mysql_native_password BY '';
FLUSH PRIVILEGES;