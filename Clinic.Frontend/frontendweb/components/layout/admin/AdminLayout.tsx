'use client';
import React, { ReactNode } from 'react';
import { Layout, Menu, Typography, Button } from 'antd';
import { UserOutlined, HomeOutlined, LogoutOutlined, AuditOutlined } from '@ant-design/icons';
import styles from './AdminLayout.module.css';
import { useRouter } from 'next/navigation';
import { useCookies } from 'react-cookie';

const { Header, Sider, Content } = Layout;
const { Title } = Typography;

interface AdminLayoutProps {
  children: ReactNode;
}

const AdminLayout: React.FC<AdminLayoutProps> = ({ children }) => {
  const router = useRouter();
  const [cookies, , removeCookie] = useCookies(['isAuth']);

  const handleLogout = () => {
    removeCookie('isAuth', { path: '/' });
    router.push('/');
  };

  return (
    <Layout className={styles.adminLayout}>
      <Sider collapsible theme="dark">
        <div className={styles.logo}>Админ Панель</div>
        <Menu theme="dark" defaultSelectedKeys={['dashboard']} mode="inline">
          <Menu.Item key="dashboard" icon={<HomeOutlined />} onClick={() => router.push('/admin')}>
            Главная
          </Menu.Item>
          <Menu.Item key="users" icon={<UserOutlined />} onClick={() => router.push('/admin/employees')}>
            Персонал
          </Menu.Item>
          <Menu.Item key="employeesDepartment" icon={<AuditOutlined />} onClick={() => router.push('/admin/employees-department')}>
            Назначения
          </Menu.Item>
        </Menu>
      </Sider>

      <Layout className={styles.siteLayout}>
        <Header className={styles.siteHeader}>
          <Title level={4} className={styles.headerTitle}>
          </Title>
          <Button type="primary" icon={<LogoutOutlined />} onClick={handleLogout}>
            Выйти
          </Button>
        </Header>

        <Content className={styles.siteContent}>{children}</Content>
      </Layout>
    </Layout>
  );
};

export default AdminLayout;
