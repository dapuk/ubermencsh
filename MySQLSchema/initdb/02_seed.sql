-- Dumping data for table ubermensch.counterparty: ~5 rows (approximately)
INSERT INTO counterparty (id_counterparty, counterparty_name, counterparty_code, rating, persentase_spread) VALUES
	(1, 'PT Bank Mandiri (Persero) Tbk.', 'BMAN', 'A', 0.5000),
	(2, 'PT Bank Central Asia Tbk.', 'BBCA', 'B', 0.4000),
	(3, 'PT Bank Negara Indonesia (Persero) Tbk.', 'BBNI', 'C', 0.3000),
	(4, 'PT Bank Pembangunan Daerah Jawa Barat dan Banten, Tbk.', 'BBJB', 'D', 0.2000),
	(5, 'PERTAMINA', 'MINA', 'A', 0.5000);

-- Dumping data for table ubermensch.quota_series: ~5 rows (approximately)
INSERT INTO quota_series (id_quota_series, series, quota, last_update) VALUES
	(1, 'FR007', 1000, '2025-10-27 17:17:54'),
	(2, 'FR008', 2000, '2025-10-27 17:17:54'),
	(3, 'FR008', 3000, '2025-10-27 17:17:54'),
	(4, 'FR008', 4000, '2025-10-27 17:17:54'),
	(5, 'FR008', 5000, '2025-10-27 17:17:54');