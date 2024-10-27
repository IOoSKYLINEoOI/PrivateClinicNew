'use client';
import React, { useEffect, useState } from 'react';
import { Card, Col, Row, Spin, message } from 'antd';
import { DepartmentResponse } from '@/models/Department';
import { getDepartments } from '@/utils/api';
import styles from './page.module.css';

const DepartmentsPage: React.FC = () => {
  const [departments, setDepartments] = useState<DepartmentResponse[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const fetchDepartments = async () => {
    setLoading(true);
    try {
      const data = await getDepartments();
      setDepartments(data);
    } catch (err) {
      console.error(err);
      message.error('Не удалось загрузить департаменты');
      setDepartments([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchDepartments();
  }, []);

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>Наши отделения</h1>
      {loading ? (
        <Spin />
      ) : departments.length === 0 ? (
        <p>Департаменты не найдены.</p>
      ) : (
        <Row gutter={16}>
          {departments.map((department) => (
            <Col span={8} key={department.id}>
              <Card
                title={department.name}
                className={styles.departmentCard}
                hoverable
                onClick={() => message.info(`Вы выбрали: ${department.name}`)}
              >
                <p><strong>Описание:</strong> {department.description}</p>
                <p><strong>Адрес:</strong> {`${department.address.country}, ${department.address.region}, ${department.address.city}, ${department.address.street}, ${department.address.houseNumber}${department.address.apartmentNumber ? `, кв. ${department.address.apartmentNumber}` : ''}${department.address.pavilion ? `, пав. ${department.address.pavilion}` : ''}`}</p>
                <p>Нажмите &quot;Подробнее&quot;, чтобы узнать больше.</p> 
              </Card>
            </Col>
          ))}
        </Row>
      )}
    </div>
  );
};

export default DepartmentsPage;
