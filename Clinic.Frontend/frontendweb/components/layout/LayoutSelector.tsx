"use client";

import { usePathname } from 'next/navigation';
import AdminLayout from './admin/AdminLayout';
import ClientLayout from './client/ClientLayout';


export default function LayoutSelector({ children }: { children: React.ReactNode }) {
  const pathname = usePathname();
  const isAdminPage = pathname.startsWith('/admin');

  return isAdminPage ? <AdminLayout>{children}</AdminLayout> : <ClientLayout>{children}</ClientLayout>;
}
