import LayoutSelector from '@/components/layout/LayoutSelector';
import '../styles/globals.css';
import { siteMetadata } from './metadata';
import { Roboto } from 'next/font/google';

const roboto = Roboto({
  subsets: ['latin'],
  weight: ['400', '700'],
  style: ['normal', 'italic'],
});

export const metadata = siteMetadata;

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="ru">
      <head />
      <body className={roboto.className}>
        <LayoutSelector>{children}</LayoutSelector>
      </body>
    </html>
  );
}
