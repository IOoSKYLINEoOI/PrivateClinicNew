'use client';
import { Carousel, Card, Row, Col, Typography, Button } from 'antd';
import { UserOutlined, ScheduleOutlined, SettingOutlined } from '@ant-design/icons';
import styles from './page.module.css';
import Link from 'next/link';
import Image from 'next/image';

const { Title, Paragraph } = Typography;

// Определение элементов меню с использованием иконок
const services = [
  {
    title: 'Терапевтические услуги',
    description: 'Профессиональные консультации терапевтов для диагностики и лечения заболеваний.',
    icon: <UserOutlined style={{ fontSize: '48px', color: '#1890ff' }} />,
    link: '/services/therapy',
  },
  {
    title: 'Диагностические процедуры',
    description: 'Современные методы диагностики для точного выявления заболеваний.',
    icon: <ScheduleOutlined style={{ fontSize: '48px', color: '#1890ff' }} />,
    link: '/services/diagnostics',
  },
  {
    title: 'Хирургические операции',
    description: 'Опытные хирурги и современное оборудование для проведения безопасных операций.',
    icon: <SettingOutlined style={{ fontSize: '48px', color: '#1890ff' }} />,
    link: '/services/surgery',
  },
];

const testimonials = [
  {
    name: 'Иван Иванов',
    feedback: 'Отличная клиника! Профессиональный персонал и современное оборудование.',
  },
  {
    name: 'Мария Смирнова',
    feedback: 'Быстрая запись на прием и внимательное отношение врачей.',
  },
  {
    name: 'Алексей Петров',
    feedback: 'Довольны результатами лечения. Рекомендую всем!',
  },
];

export default function HomePage() {
  return (
    <div>
      {/* Hero Section */}
      <Carousel autoplay className={styles.carousel}>
        <div className={styles.carouselSlide}>
          <Image
            src="/images/clinicHomeCarousel1.jpg" // Исправленный путь к изображению
            alt="Клиника МЕДЕКОН"
            fill
            className={styles.carouselImage}
            priority
          />
          <div className={styles.carouselContent}>
            <Title level={2} style={{ color: '#fff' }}>Добро пожаловать в МЕДЕКОН</Title>
            <Paragraph style={{ color: '#fff', fontSize: '18px' }}>
              Высококачественные медицинские услуги для всей семьи.
            </Paragraph>
            <Button type="primary" size="large">
              Записаться на прием
            </Button>
          </div>
        </div>
        <div className={styles.carouselSlide}>
          <Image
            src="/images/clinicHomeCarousel2.jpg" 
            alt="Медицинское оборудование"
            fill
            className={styles.carouselImage}
            priority
          />
          <div className={styles.carouselContent}>
            <Title level={2} style={{ color: '#fff' }}>Современное оборудование</Title>
            <Paragraph style={{ color: '#fff', fontSize: '18px' }}>
              Используем только самое современное медицинское оборудование.
            </Paragraph>
            <Button type="primary" size="large">
              Узнать больше
            </Button>
          </div>
        </div>
      </Carousel>

      {/* Services Section */}
      <div className={styles.section}>
        <Title level={2} style={{ textAlign: 'center' }}>Наши Услуги</Title>
        <Row gutter={[16, 16]} justify="center">
          {services.map((service, index) => (
            <Col xs={24} sm={12} md={8} key={index}>
              <Card
                hoverable
                className={styles.card}
                bordered={false}
                cover={<div className={styles.cardIcon}>{service.icon}</div>}
              >
                <Title level={4}>{service.title}</Title>
                <Paragraph>{service.description}</Paragraph>
                <Link href={service.link}>
                  <Button type="link">Подробнее</Button>
                </Link>
              </Card>
            </Col>
          ))}
        </Row>
      </div>

      {/* About Us Section */}
      <div className={styles.section} style={{ backgroundColor: '#f0f2f5' }}>
        <Title level={2} style={{ textAlign: 'center' }}>О Нас</Title>
        <Row gutter={[16, 16]} justify="center" align="middle">
          <Col xs={24} md={12}>
            <Image
              src="/images/clinicHomeAboutUs.jpg"
              alt="О нас"
              width={400}
              height={200}
              className={styles.aboutImage}
              priority
            />
          </Col>
          <Col xs={24} md={12}>
            <Paragraph>
              Клиника МЕДЕКОН предоставляет широкий спектр медицинских услуг с акцентом на качество и комфорт пациентов. Наши врачи — высококвалифицированные специалисты с многолетним опытом работы.
            </Paragraph>
            <Paragraph>
              Мы стремимся обеспечить максимально эффективное лечение, используя передовые технологии и методы диагностики.
            </Paragraph>
            <Link href="/about">
              <Button type="primary">Узнать больше</Button>
            </Link>
          </Col>
        </Row>
      </div>

      {/* Testimonials Section */}
      <div className={styles.section}>
        <Title level={2} style={{ textAlign: 'center' }}>Отзывы Пациентов</Title>
        <Row gutter={[16, 16]} justify="center">
          {testimonials.map((testimonial, index) => (
            <Col xs={24} sm={12} md={8} key={index}>
              <Card bordered={false} className={styles.testimonialCard}>
                <Paragraph>&quot;{testimonial.feedback}&quot;</Paragraph> {/* Экранированные кавычки */}
                <Title level={5} style={{ textAlign: 'right' }}>— {testimonial.name}</Title>
              </Card>
            </Col>
          ))}
        </Row>
      </div>

      {/* Contact Section */}
      <div className={styles.section} style={{ backgroundColor: '#f0f2f5' }}>
        <Title level={2} style={{ textAlign: 'center' }}>Свяжитесь с Нами</Title>
        <div style={{ textAlign: 'center', marginTop: '20px' }}>
          <Button type="primary" size="large">
            Записаться на прием
          </Button>
        </div>
      </div>
    </div>
  );
}
