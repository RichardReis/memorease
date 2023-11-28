import React from "react";
import { MaterialCommunityIcons } from "@expo/vector-icons";

const Icon: React.FC<{
  color: string;
  name: React.ComponentProps<typeof MaterialCommunityIcons>["name"];
  size?: number;
}> = ({ color, name, size }) => {
  return <MaterialCommunityIcons color={color} name={name} size={size || 28} />;
};

export default Icon;
