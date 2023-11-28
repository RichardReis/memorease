import React from 'react';
import { Text } from 'react-native';

interface FormErrorProps {
  show: boolean;
}

const FormError: React.FC<FormErrorProps> = ({ show }) => {
  if (!show) return <></>;
  return <Text>Este campo é obrigatorio.</Text>;
};

export default FormError;
