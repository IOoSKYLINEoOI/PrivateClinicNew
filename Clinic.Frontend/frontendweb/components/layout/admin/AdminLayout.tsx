'use client';
import React, { ReactNode } from 'react';
import { Layout, Menu, Typography, Button } from 'antd';
import { UserOutlined, HomeOutlined, LogoutOutlined, AuditOutlined, CalendarOutlined, CheckOutlined, SwapOutlined, CompressOutlined } from '@ant-design/icons';
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
          <Menu.Item key="schedule" icon={<CalendarOutlined />} onClick={() => router.push('/admin/schedule')}>
            Штатное расписание
          </Menu.Item>
          <Menu.Item key="diagnostic" icon={<CheckOutlined />} onClick={() => router.push('/admin/diagnostic')}>
            Анализы
          </Menu.Item>
          <Menu.Item key="appointment" icon={<SwapOutlined />} onClick={() => router.push('/admin/appointment')}>
            Записи
          </Menu.Item>
          <Menu.Item key="resultICD" icon={<CompressOutlined />} onClick={() => router.push('/admin/resultICD')}>
            Результаты
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
