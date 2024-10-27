
import { Layout, Menu, Button } from 'antd';
import { UserOutlined, ScheduleOutlined, SettingOutlined } from '@ant-design/icons';
import { useState, useEffect } from 'react';
import styles from './ClientLayout.module.css';
import Link from 'next/link';
import { useCookies } from 'react-cookie';
import { logoutUser } from '@/utils/api';
import AuthModal from '@/components/auth/AuthModal';

const { Header, Content, Footer } = Layout;

export default function ClientLayout({ children }: { children: React.ReactNode }) {
  const [isAuthModalVisible, setAuthModalVisible] = useState(false);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [cookies, setCookie] = useCookies(['isAuth']);

  useEffect(() => {
    const isAuth = cookies['isAuth'];
    setIsAuthenticated(isAuth);
  }, [cookies]);

  const showAuthModal = () => setAuthModalVisible(true);
  const closeAuthModal = () => setAuthModalVisible(false);

  const handleLogout = async () => {
    try {
      await logoutUser();
      setCookie('isAuth', false, {
        path: '/',
        maxAge: 86400,
        secure: true,
        sameSite: 'strict'
      });
      setIsAuthenticated(false);
    } catch (error) {
      console.error('Ошибка при выходе из системы', error);
    }
  };

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header style={{ display: 'flex', alignItems: 'center' }}>
        <div className={styles.logo}>Медекон</div>
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={['1']}
          items={[
            { key: '1', icon: <UserOutlined />, label: <Link href="/">Главная</Link> },
            { key: '2', icon: <ScheduleOutlined />, label: <Link href="/appointments">Записи</Link> },
            { key: '3', icon: <SettingOutlined />, label: <Link href="/departments">Отделения</Link> },
          ]}
          style={{ flexGrow: 1 }}
        />
        {isAuthenticated ? (
          <>
            <Button type="default" className={styles.profileButton}>
              <Link href="/profile">Профиль</Link>
            </Button>
            <Button type="primary" onClick={handleLogout}>Выход</Button>
          </>
        ) : (
          <>
            <Button type="primary" onClick={showAuthModal}>Вход / Регистрация</Button>
            <AuthModal isVisible={isAuthModalVisible} onClose={closeAuthModal} />
          </>
        )}
      </Header>
      <Content className={styles.content}>
        {children}
      </Content>
      <Footer className={styles.footer}>
        {/* Footer Content */}
      </Footer>
    </Layout>
  );
}
