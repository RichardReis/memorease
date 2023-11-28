import React from "react";
import { Text, TouchableOpacity } from "react-native";
import ButtonStyles, { ButtonTextStyles } from "../../themedStyles/Button";
import Icon from "../Icon";
import { MaterialCommunityIcons } from "@expo/vector-icons";
import Colors from "../../constants/Colors";

interface ButtonProps {
  title?: string;
  onPress?: (event: any) => void;
  type: "primary" | "secondary" | "success" | "danger" | "neutral" | "warning";
  icon?: React.ComponentProps<typeof MaterialCommunityIcons>["name"];
  size?: number;
}

const Button: React.FC<ButtonProps> = ({
  icon,
  title,
  onPress,
  type,
  size,
}) => {
  const BUTTON_SIZE = !!size ? { width: size, height: size } : {};

  return (
    <TouchableOpacity
      style={{ ...ButtonStyles.base, ...ButtonStyles[type], ...BUTTON_SIZE }}
      onPress={onPress}
    >
      {icon && (
        <Icon
          color={
            type != "secondary" ? Colors["light"].white : Colors["light"].text
          }
          name={icon}
        />
      )}
      {title && <Text style={{ ...ButtonTextStyles[type] }}>{title}</Text>}
    </TouchableOpacity>
  );
};

export default Button;
