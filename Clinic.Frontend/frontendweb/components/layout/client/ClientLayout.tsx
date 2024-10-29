
import { Layout, Menu, Button } from 'antd';
import { UserOutlined, ScheduleOutlined, FacebookFilled, InstagramFilled, TwitterSquareFilled, ShopOutlined, UsergroupAddOutlined, CheckOutlined } from '@ant-design/icons';
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
            { key: '2', icon: <ShopOutlined />, label: <Link href="/departments">Отделения</Link> },
            { key: '3', icon: <UsergroupAddOutlined />, label: <Link href="/employee">Врачи</Link> },
            { key: '4', icon: <CheckOutlined />, label: <Link href="/diagnostic">Анализы</Link> },
            { key: '5', icon: <ScheduleOutlined />, label: <Link href="/appointments">Запись на приём</Link> },            
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
      <div className={styles.footerContainer}>
          <div className={styles.footerSection}>
            <h3>Контакты</h3>
            <p>Телефон: +7 (XXX) XXX-XX-XX</p>
            <p>Email: info@medekon.ru</p>
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
