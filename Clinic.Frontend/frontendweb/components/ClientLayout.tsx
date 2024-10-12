// app/ClientLayout.tsx
'use client';

import { Layout, Menu } from 'antd';
import { UserOutlined, ScheduleOutlined, SettingOutlined, FacebookFilled, InstagramFilled, TwitterSquareFilled } from '@ant-design/icons';
import styles from './ClientLayout.module.css';
import Link from 'next/link';

const { Header, Content, Footer } = Layout;

// Определение элементов меню с использованием иконок
const menuItems = [
  { key: '1', label: 'Главная', icon: <UserOutlined />, path: '/' },
  { key: '2', label: 'Записи', icon: <ScheduleOutlined />, path: '/appointments' },
  { key: '3', label: 'Отделения', icon: <SettingOutlined />, path: '/departments' },
];

export default function ClientLayout({ children }: { children: React.ReactNode }) {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header style={{ display: 'flex', alignItems: 'center' }}>
        <div className={styles.logo}>Медкон</div>
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={['1']}
          style={{ flexGrow: 1 }}
        >
          {menuItems.map(item => (
            <Menu.Item key={item.key} icon={item.icon}>
              <Link href={item.path}>{item.label}</Link>
            </Menu.Item>
          ))}
        </Menu>
      </Header>
      <Content className={styles.content}>
        {children}
      </Content>
      <Footer className={styles.footer}>
        <div className={styles.footerContainer}>
          <div className={styles.footerSection}>
            <h3>Контакты</h3>
            <p>Телефон: +7 (123) 456-78-90</p>
            <p>Email: info@medkon.ru</p>
          </div>
          <div className={styles.footerSection}>
            <h3>Социальные сети</h3>
            <div className={styles.socialIcons}>
              <FacebookFilled className={styles.icon} />
              <InstagramFilled className={styles.icon} />
              <TwitterSquareFilled className={styles.icon} />
            </div>
          </div>
          <div className={styles.footerSection}>
            <p>© {new Date().getFullYear()} Медкон - Все права защищены</p>
          </div>
        </div>
      </Footer>
    </Layout>
  );
}
