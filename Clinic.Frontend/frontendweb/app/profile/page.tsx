'use client';
import { useEffect, useState } from 'react';
import { Avatar, Typography, Card, Row, Col } from 'antd';
import styles from './page.module.css';
import { UserResponse } from '@/models/User';
import { getUserProfile } from '@/utils/api';

const { Title, Text } = Typography;

const ProfilePage: React.FC = () => {
  const [user, setUser] = useState<UserResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const data = await getUserProfile();
        setUser(data);
      } catch (error) {
        setError('Ошибка при загрузке профиля');
      } finally {
        setLoading(false);
      }
    };

    fetchProfile();
  }, []);

  if (loading) {
    return <div className={styles.spinner}>Загрузка...</div>;
  }

  if (error) {
    return <p className={styles.error}>{error}</p>;
  }

  return (
    <div className={styles.profileContainer}>
      <Row gutter={16}>
        <Col span={8}>
          <Card className={styles.avatarCard}>
            <Avatar size={128} src={user?.imageId ? `/api/images/${user.imageId}` : undefined} />
          </Card>
        </Col>
        <Col span={16}>
          <Card className={styles.infoCard}>
            <Title level={2} className={styles.name}>
              {user?.lastName} {user?.firstName} {user?.fatherName}
            </Title>
            <Text className={styles.email}>{user?.email}</Text>
            <Row>
              <Col span={12}>
                <p><strong>Телефон:</strong> {user?.phoneNumber}</p>
              </Col>
              <Col span={12}>
                <p><strong>Дата рождения:</strong> {user?.dateOfBirth.toString()}</p>
              </Col>
            </Row>
          </Card>
        </Col>
      </Row>
    </div>
  );
};

export default ProfilePage;
