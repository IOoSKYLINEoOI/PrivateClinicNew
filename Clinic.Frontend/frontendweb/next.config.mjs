import fs from 'fs';
import path from 'path';

/** @type {import('next').NextConfig} */
const nextConfig = {
  serverRuntimeConfig: {
    ssl: {
      key: fs.readFileSync(path.join(process.cwd(), 'certs/localhost-key.pem')),
      cert: fs.readFileSync(path.join(process.cwd(), 'certs/localhost.pem')),
    },
  },
  // Добавляем стандартные опции
  reactStrictMode: true,
};

export default nextConfig;
