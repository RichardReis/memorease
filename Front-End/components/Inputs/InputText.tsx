import React from "react";
import {
  NativeSyntheticEvent,
  TextInput,
  TextInputFocusEventData,
  View,
} from "react-native";
import InputStyles from "../../themedStyles/Input";

type InputTextType = {
  placeholder?: string;
  onBlur?: (e: NativeSyntheticEvent<TextInputFocusEventData>) => void;
  onChange?: (text: string) => void;
  value?: string;
  type?: "text" | "password";
};

const InputText: React.FC<InputTextType> = ({
  placeholder,
  onBlur,
  onChange,
  value,
  type = "text",
}) => {
  return (
    <TextInput
      placeholder={placeholder}
      onBlur={onBlur}
      onChangeText={onChange}
      value={value}
      secureTextEntry={type === "password"}
      style={{ ...InputStyles.regular }}
    />
  );
};

export default InputText;
