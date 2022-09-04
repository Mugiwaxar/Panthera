using Panthera.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Passives
{
    class SuperSprint
    {
        public static void StartSuperSprint(PantheraMotor motor)
        {
            if (motor.superSprinting == true) return;
            motor.superSprinting = true;
            motor.characterBody.moveSpeed += PantheraConfig.superSprintSpeedAdded;
            motor.characterBody.baseMoveSpeed += PantheraConfig.superSprintSpeedAdded;
            Utils.Sound.playSound(Utils.Sound.Dash, motor.gameObject);
            motor.pantheraObj.pantheraFX.SetSuperSprintFX(true);
            motor.superSprinting = true;
        }

        public static void StopSuperSpring(PantheraMotor motor)
        {
            if (motor.superSprinting == false) return;
            motor.superSprinting = false;
            motor.characterBody.moveSpeed -= PantheraConfig.superSprintSpeedAdded;
            motor.characterBody.baseMoveSpeed -= PantheraConfig.superSprintSpeedAdded;
            motor.pantheraObj.pantheraFX.SetSuperSprintFX(false);
            motor.superSprinting = false;
        }

    }
}
